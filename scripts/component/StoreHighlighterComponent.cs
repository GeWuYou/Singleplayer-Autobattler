using System.Threading.Tasks;
using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Core.Godot.extensions;
using Godot;
using SingleplayerAutobattler.scripts.architecture;

namespace SingleplayerAutobattler.scripts.component;

/// <summary>
/// 商店高亮组件，用于在玩家区域中高亮显示当前悬停的瓦片。
/// 继承自Godot.Node并实现IController接口。
/// </summary>
public partial class StoreHighlighterComponent : Node, IController
{
    private bool _enable = true;

    /// <summary>
    /// 是否启用高亮功能。设置该属性会触发异步初始化逻辑。
    /// </summary>
    [Export]
    public bool Enable
    {
        get => _enable;
        set
        {
            _enable = value;
            _ = SetEnable(); // 异步更新状态
        }
    }

    /// <summary>
    /// 根据Enable属性的状态执行相关操作。
    /// 当禁用且PlayerArea有效时清除高亮层。
    /// </summary>
    private async Task SetEnable()
    {
        await this.WaitUntilReady();
        if (!Enable && PlayerArea.IsValidNode())
        {
            HighlightLayer!.Clear();
        }
    }

    /// <summary>
    /// 玩家区域组件引用，用于获取鼠标悬停位置等信息。
    /// </summary>
    [Export] public PlayerAreaComponent? PlayerArea { get; set; }

    /// <summary>
    /// 高亮图层TileMapLayer引用，用于绘制高亮效果。
    /// </summary>
    [Export] public TileMapLayer? HighlightLayer { get; set; }

    /// <summary>
    /// 当前悬停的瓦片坐标（在TileSet中的局部坐标）。
    /// </summary>
    [Export] public Vector2I HoveredTile { get; set; }

    public int SourceId { get; set; }

    /// <summary>
    /// 获取游戏架构实例
    /// </summary>
    /// <returns>返回游戏架构接口实例</returns>
    public IArchitecture GetArchitecture() => GameArchitecture.Instance;

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        // 获取TileSet的第一个Source ID以供后续使用
        SourceId = PlayerArea!.TileSet.GetSourceId(0);
    }

    /// <summary>
    /// 每帧处理逻辑，在此检查是否需要更新高亮瓦片。
    /// 若未启用则直接返回；若悬停位置无效则清空高亮。
    /// </summary>
    /// <param name="delta">与上一帧之间的时间差（秒）</param>
    public override void _Process(double delta)
    {
        if(!Enable)return;

        // 获取当前鼠标悬停的瓦片
        var selectTile = PlayerArea!.GetHoveredTile();

        // 判断瓦片是否越界
        if (!PlayerArea.IsTileInBounds(selectTile))
        {
            HighlightLayer!.Clear();
            return;
        }

        // 更新高亮瓦片
        UpdateTile(selectTile);
    }

    /// <summary>
    /// 更新指定瓦片的高亮状态。
    /// 清除之前的高亮，并将新的高亮应用到目标瓦片。
    /// </summary>
    /// <param name="selectTile">要高亮的目标瓦片坐标</param>
    private void UpdateTile(Vector2I selectTile)
    {
        HighlightLayer!.Clear();
        HighlightLayer!.SetCell(selectTile,SourceId,HoveredTile);
    }
}
