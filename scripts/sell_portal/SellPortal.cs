using Godot;
using SingleplayerAutobattler.scripts.component;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.player;
using Unit = SingleplayerAutobattler.scripts.unit.Unit;

namespace SingleplayerAutobattler.scripts.sell_portal;

/// <summary>
///     出售传送门类，用于处理单位的出售功能
///     继承自Area2D并实现IController接口
/// </summary>
[ContextAware]
[Log]
public partial class SellPortal : Area2D, IController
{
    private Unit? _currentUnit;
    [Export] public PlayerDataResource PlayerDataResource { get; set; } = null!;
    [Export] public OutlineHighlighter OutlineHighlighter { get; set; } = null!;
    [Export] public HBoxContainer GoldContainer { get; set; } = null!;
    [Export] public Label GoldLabel { get; set; } = null!;

    /// <summary>
    ///     节点准备就绪时的回调方法
    ///     在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        // 连接区域进入和退出事件
        AreaEntered += OnAreaEntered;
        AreaExited += OnAreaExited;
        // 获取场景中所有单位并设置它们的出售功能
        var units = GetTree().GetNodesInGroup(GroupConstants.Units).OfType<Unit>();
        foreach (var unit in units) SetupUnit(unit);
    }

    /// <summary>
    ///     为指定单位设置出售相关功能
    /// </summary>
    /// <param name="unit">需要设置的单位对象</param>
    public void SetupUnit(Unit unit)
    {
        unit.DragDropComponent
            .Signal(DragDropComponent.SignalName.Dropped)
            .To(Callable.From<Vector2>(startingPosition => OnUnitDropped(startingPosition, unit)))
            .End();
        unit
            .Signal(Unit.SignalName.QuickSellPressed)
            .To(Callable.From(() => OnSellUnit(unit)))
            .End();
    }

    /// <summary>
    ///     当单位被拖放时的回调方法
    /// </summary>
    /// <param name="_">起始位置参数（未使用）</param>
    /// <param name="unit">被拖放的单位对象</param>
    private void OnUnitDropped(Vector2 _, Unit unit)
    {
        if (unit.IsValidNode() && unit == _currentUnit) OnSellUnit(unit);
    }

    /// <summary>
    ///     出售指定单位的方法
    /// </summary>
    /// <param name="unit">需要出售的单位对象</param>
    private void OnSellUnit(Unit unit)
    {
        PlayerDataResource.Gold += unit.UnitDataResource!.GetGoldValue();
        _log.Info($"Sold {unit.UnitDataResource!.Name} for {unit.UnitDataResource!.GetGoldValue()} gold");
        _log.Info("Player Gold is {0}", PlayerDataResource.Gold);
        unit.QueueFreeX();
    }

    /// <summary>
    ///     当区域退出时的回调方法
    /// </summary>
    /// <param name="area">退出的区域对象</param>
    private void OnAreaExited(Area2D area)
    {
        area.IfType<Unit>(unit =>
            {
                if (unit == _currentUnit) _currentUnit = null;

                OutlineHighlighter.ClearHighlight();
                GoldContainer.Hide();
            }
        );
    }

    /// <summary>
    ///     当区域进入时的回调方法
    /// </summary>
    /// <param name="area">进入的区域对象</param>
    private void OnAreaEntered(Area2D area)
    {
        area.IfType<Unit>(unit =>
            {
                _currentUnit = unit;
                OutlineHighlighter.Highlight();
                GoldLabel.Text = unit.UnitDataResource!.GetGoldValue().ToString();
                GoldContainer.Show();
            }
        );
    }
}