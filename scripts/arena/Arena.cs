using Godot;
using SingleplayerAutobattler.scripts.component;
using SingleplayerAutobattler.scripts.sell_portal;
using SingleplayerAutobattler.scripts.unit;
using Unit = SingleplayerAutobattler.scripts.unit.Unit;

namespace SingleplayerAutobattler.scripts.arena;

[ContextAware]
[Log]
public partial class Arena : Node2D, IController
{
    [Export] public UnitDataResource? InitialSpawnUnitDataResource { get; set; }
    [Export] public UnitSpawnerComment UnitSpawnerComment { get; set; } = null!;
    [Export] public UnitMoverComponent UnitMoverComponent { get; set; } = null!;
    [Export] public SellPortal SellPortal { get; set; } = null!;

    /// <summary>
    ///     节点准备就绪时的回调方法
    ///     在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        UnitSpawnerComment
            .Signal(UnitSpawnerComment.SignalName.UnitSpawned)
            .To(Callable.From<Unit>(unit => UnitMoverComponent.SetupUnit(unit)))
            .To(Callable.From<Unit>(unit => SellPortal.SetupUnit(unit)))
            .End();

        if (InitialSpawnUnitDataResource is not null) UnitSpawnerComment.SpawnUnit(InitialSpawnUnitDataResource);
    }
}
