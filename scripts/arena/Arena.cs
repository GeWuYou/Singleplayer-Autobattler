using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Core.events;
using GFramework.Core.extensions;
using Godot;
using SingleplayerAutobattler.scripts.architecture;
using SingleplayerAutobattler.scripts.component;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.arena;
public partial class Arena :Node2D,IController
{
	/// <summary>
	/// 取消注册列表，用于管理需要在节点销毁时取消注册的对象
	/// </summary>
	private IUnRegisterList _unRegisterList = new UnRegisterList();
	
	/// <summary>
	/// 获取游戏架构实例
	/// </summary>
	/// <returns>返回游戏架构接口实例</returns>
	public IArchitecture GetArchitecture() => GameArchitecture.Instance;
	
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

	/// <summary>
	/// 节点退出场景树时的回调方法
	/// 在节点从场景树移除前调用，用于清理资源
	/// </summary>
	public override void _ExitTree() => _unRegisterList.UnRegisterAll();
}


