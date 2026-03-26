using Godot;
using SingleplayerAutobattler.scripts.enums.scene;
using SingleplayerAutobattler.scripts.unit;
using Unit = SingleplayerAutobattler.scripts.unit.Unit;

namespace SingleplayerAutobattler.scripts.component;

/// <summary>
///     单位生成器组件，用于在游戏区域或备战区中生成单位。
///     实现了 IController 接口以接入游戏架构系统。
/// </summary>
[ContextAware]
[Log]
public partial class UnitSpawnerComment : Node, IController
{
    /// <summary>
    ///     当一个新单位被成功生成时触发的信号事件。
    /// </summary>
    /// <param name="unit">刚生成的单位实例。</param>
    [Signal]
    public delegate void UnitSpawnedEventHandler(Unit unit);

    /// <summary>
    ///     可导出属性：玩家的备战区组件引用。
    /// </summary>
    [Export]
    public PlayerAreaComponent? BenchArea { get; set; }

    /// <summary>
    ///     可导出属性：玩家的游戏区（战场）组件引用。
    /// </summary>
    [Export]
    public PlayerAreaComponent? GameArea { get; set; }

    /// <summary>
    ///     查找第一个有空位的区域（优先检查备战区，其次为游戏区）。
    /// </summary>
    /// <returns>如果有可用区域则返回对应的 <see cref="PlayerAreaComponent" />；否则返回 null。</returns>
    public PlayerAreaComponent? GetFirstAvailableArea()
    {
        // 检查备战区是否有空位
        if (!BenchArea!.UnitGrid!.IsGridFull()) return BenchArea;

        // 否则检查游戏区是否还有空位
        return !GameArea!.UnitGrid!.IsGridFull() ? GameArea : null;
    }

    /// <summary>
    ///     根据提供的单位数据资源，在合适的区域内生成一个新的单位。
    /// </summary>
    /// <param name="unitDataResource">描述要创建单位的数据资源对象。</param>
    public void SpawnUnit(UnitDataResource? unitDataResource)
    {
        if (unitDataResource is null)
        {
            _log.Error("单位数据为空，无法生成单位");
            return;
        }

        // 获取第一个可放置单位的区域
        var area = GetFirstAvailableArea();
        if (area is null)
        {
            _log.Error("无法添加单位");
            return;
        }

        // 获取该区域的网格管理器并查找首个空格子
        var grid = area.UnitGrid!;
        var tile = grid.GetFirstEmptyTile();
        if (tile == new Vector2I(-1, -1))
        {
            _log.Error("未找到可用格子，无法生成单位");
            return;
        }

        var sceneRegistry = this.GetUtility<IGodotSceneRegistry>()!;
        var newUnit = sceneRegistry.Get(nameof(SceneKey.Unit)).Instantiate<Unit>();

        // 先绑定数据，再写入网格模型，避免模型读取到空数据。
        newUnit.UnitDataResource = unitDataResource;
        grid.AddChild(newUnit);
        grid.AddUnit(tile, newUnit);
        newUnit.GlobalPosition = area.GetGlobalFromTile(tile) - ArenaConstants.HalfCellSizeVector;
        EmitSignalUnitSpawned(newUnit);
    }
}