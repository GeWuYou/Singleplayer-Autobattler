using GFramework.Core.Abstractions.controller;
using GFramework.Core.extensions;
using GFramework.Game.Abstractions.assets;
using GFramework.Godot.extensions.signal;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;
using Godot;
using SingleplayerAutobattler.scripts.component;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.sell_portal;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.arena;

[ContextAware]
[Log]
public partial class Arena : Node2D, IController
{
    [Export] public UnitSpawnerComment UnitSpawnerComment { get; set; } = null!;
    [Export] public UnitMoverComponent UnitMoverComponent { get; set; } = null!;
    [Export] public SellPortal SellPortal { get; set; } = null! ;
    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        UnitSpawnerComment
            .Signal(UnitSpawnerComment.SignalName.UnitSpawned)
            .To(Callable.From<Unit>(unit => UnitMoverComponent.SetupUnit(unit)))
            .To(Callable.From<Unit>(unit => SellPortal.SetupUnit(unit)))
            .End();
        var resourceFactorySystem = this.GetSystem<IResourceFactorySystem>();
        UnitSpawnerComment.SpawnUnit(resourceFactorySystem!
            .GetFactory<UnitDataResource>(AssetCatalogConstants.AssetCatalogResource.Robin).Invoke());
    }
}