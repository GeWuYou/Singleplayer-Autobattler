using System.Linq;
using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Godot.extensions;
using Godot;
using SingleplayerAutobattler.scripts.architecture;
using SingleplayerAutobattler.scripts.component;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.player;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.sell_portal;

public partial class SellPortal : Area2D, IController
{
    /// <summary>
    /// 获取游戏架构实例
    /// </summary>
    /// <returns>返回游戏架构接口实例</returns>
    public IArchitecture GetArchitecture() => GameArchitecture.Instance;

    [Export] public PlayerDataResource? PlayerDataResource { get; set; }
    [Export] public OutlineHighlighter? OutlineHighlighter { get; set; }
    [Export] public HBoxContainer? GoldContainer { get; set; }
    [Export] public Label? GoldLabel { get; set; }
    private Unit? _currentUnit;

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
        AreaExited += OnAreaExited;
        var units = GetTree().GetNodesInGroup(GroupConstants.Units).OfType<Unit>();
        foreach (var unit in units)
        {
            SetupUnit(unit);
        }
    }

    private void SetupUnit(Unit unit)
    {
        unit.DragDropComponent!.Connect(DragDropComponent.SignalName.DragStarted,
            Callable.From<Vector2>(startingPosition => OnUnitDropped(startingPosition, unit)));
        unit.Connect(Unit.SignalName.QuickSellPressed, Callable.From(() => OnSellUnit(unit)));
    }

    private void OnUnitDropped(Vector2 _, Unit unit)
    {
        if (unit.IsValidNode() && unit == _currentUnit)
        {
            OnSellUnit(unit);
        }
    }

    private void OnSellUnit(Unit unit)
    {
        PlayerDataResource!.Gold += unit.UnitDataResource!.GetGoldValue();
        GD.Print($"Sold {unit.UnitDataResource!.Name} for {unit.UnitDataResource!.GetGoldValue()} gold");
        unit.QueueFreeX();
    }

    private void OnAreaExited(Area2D area)
    {
        
    }

    private void OnAreaEntered(Area2D area)
    {
    }
}