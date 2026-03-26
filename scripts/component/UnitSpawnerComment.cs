using Godot;
using SingleplayerAutobattler.scripts.unit;
using Unit = SingleplayerAutobattler.scripts.unit.Unit;

namespace SingleplayerAutobattler.scripts.component;

/// <summary>
///     单位生成器组件，用于在游戏区域或备战区中生成单位。
///     实现了 IController 接口以接入游戏架构系统。
/// </summary>
[ContextAware]
public partial class UnitSpawnerComment : Node, IController
{
    /// <summary>
    ///     当一个新单位被成功生成时触发的信号事件。
    /// </summary>
    /// <param name="unit">刚生成的单位实例。</param>
    [Signal]
    public delegate void UnitSpawnedEventHandler(Unit unit);


    // private IResourceFactorySystem? _resourceFactorySystem;

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
        // 获取第一个可放置单位的区域
        var area = GetFirstAvailableArea();
        if (area is null)
        {
            GD.PrintErr("无法添加单位");
            return;
        }

        // 获取该区域的网格管理器并查找首个空格子
        var grid = area.UnitGrid!;
        // todo 需要修改
        // var newUnit = _resourceFactorySystem!.GetFactory<Unit>(AssetCatalogConstants.AssetCatalogSceneUnit.Unit)
        //     .Invoke();
        // var tile = grid.GetFirstEmptyTile();
        //
        // // 将新单位加入场景树，并绑定到指定格子上
        // grid.AddChild(newUnit);
        // grid.AddUnit(tile, newUnit);
        //
        // // 设置单位的世界坐标位置
        // newUnit.GlobalPosition = area.GetGlobalFromTile(tile) - ArenaConstants.HalfCellSizeVector;
        //
        // // 绑定单位数据资源
        // newUnit.UnitDataResource = unitDataResource;
        // EmitSignalUnitSpawned(newUnit);
    }

    /// <summary>
    ///     节点准备就绪时的回调方法。
    ///     在节点添加到场景树之后自动调用，用于初始化依赖的服务系统。
    /// </summary>
    public override void _Ready()
    {
        // _resourceFactorySystem = this.GetSystem<ResourceFactorySystem>();
    }
}