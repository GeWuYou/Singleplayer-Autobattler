using GFramework.Core.Abstractions.controller;
using GFramework.Core.events;
using GFramework.Core.extensions;
using GFramework.Game.Abstractions.assets;
using GFramework.Godot.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;
using Godot;
using SingleplayerAutobattler.scripts.component;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.system;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.arena;

[ContextAware]
[GodotLog]
public partial class Arena : Node2D, IController
{
    [Export] public UnitSpawnerComment? UnitSpawnerComment { get; set; }
    [Export] public UnitMoverComponent? UnitMoverComponent { get; set; }

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        UnitSpawnerComment!.Connect(UnitSpawnerComment.SignalName.UnitSpawned,
            Callable.From<Unit>(unit => UnitMoverComponent!.SetupUnit(unit)));
        var resourceFactorySystem = this.GetSystem<IResourceFactorySystem>();
        this.RegisterEvent<ResourceFactorySystem.ResourceRegisterReady>(_ =>
        {
            _log.Info("我被调用了");
            UnitSpawnerComment.SpawnUnit(resourceFactorySystem!
                .GetFactory<UnitDataResource>(AssetCatalogConstants.AssetCatalogResource.Robin).Invoke());
        });
    }
}