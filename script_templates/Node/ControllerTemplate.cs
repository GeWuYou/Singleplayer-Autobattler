// meta-name: 控制器类模板
// meta-description: 负责管理场景的生命周期和架构关联
using Godot;
using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Core.events;
using GFramework.Core.extensions;
using SingleplayerAutobattler.script.architecture;

public partial class _CLASS_ :_BASE_,IController
{
    /// <summary>
    /// 取消注册列表，用于管理需要在节点销毁时取消注册的对象
    /// </summary>
    private readonly IUnRegisterList _unRegisterList = new UnRegisterList();
    
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
        
    }

    /// <summary>
    /// 节点退出场景树时的回调方法
    /// 在节点从场景树移除前调用，用于清理资源
    /// </summary>
    public override void _ExitTree() => _unRegisterList.UnRegisterAll();
}

