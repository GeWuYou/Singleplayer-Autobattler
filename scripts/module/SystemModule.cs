using GFramework.Core.Abstractions.architecture;
using GFramework.Game.setting;
using SingleplayerAutobattler.scripts.core.audio.system;
using SingleplayerAutobattler.scripts.core.scene;
using SingleplayerAutobattler.scripts.core.ui;
using SingleplayerAutobattler.scripts.system;
using SingleplayerAutobattler.scripts.system.impl;

namespace SingleplayerAutobattler.scripts.module;

/// <summary>
///     系统Godot模块类，负责安装和注册游戏所需的各种系统组件
/// </summary>
public class SystemModule : IArchitectureModule
{
    /// <summary>
    ///     安装方法，用于向游戏架构注册各种系统组件
    /// </summary>
    /// <param name="architecture">游戏架构接口实例，用于注册系统</param>
    public void Install(IArchitecture architecture)
    {
        architecture.RegisterSystem(new UiRouter());
        architecture.RegisterSystem(new SceneRouter());
        architecture.RegisterSystem(new SettingsSystem());
        architecture.RegisterSystem(new GodotAudioSystem());
        // 注册数据解析系统
        architecture.RegisterSystem(new DataParseSystem());
        architecture.RegisterSystem<IShopSystem>(new ShopSystem());
        architecture.RegisterSystem(new RerollSystem());
    }
}