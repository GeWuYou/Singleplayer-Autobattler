using GFramework.Core.Abstractions.controller;
using GFramework.SourceGenerators.Abstractions.rule;
using Godot;
using SingleplayerAutobattler.scripts.component;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.arena;
[ContextAware]
public partial class Arena :Node2D,IController
{
	
	[Export]
	public UnitSpawnerComment? UnitSpawnerComment { get; set; }
	[Export]
	public UnitMoverComponent? UnitMoverComponent { get; set; }
	
	/// <summary>
	/// 节点准备就绪时的回调方法
	/// 在节点添加到场景树后调用
	/// </summary>
	public override void _Ready()
	{
		UnitSpawnerComment!.Connect(UnitSpawnerComment.SignalName.UnitSpawned,Callable.From<Unit>(unit=>UnitMoverComponent!.SetupUnit(unit)));
	}
}
