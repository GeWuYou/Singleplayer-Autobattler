// meta-name: 控制器类模板
// meta-description: 负责管理场景的生命周期和架构关联
using Godot;
using GFramework.Core.architecture;
using GFramework.Core.controller;
using SingleplayerAutobattler.scripts.architecture;

public partial class _CLASS_ :_BASE_,IController
{
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
}

