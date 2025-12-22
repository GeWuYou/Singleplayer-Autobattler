using GFramework.Godot.architecture;
using SingleplayerAutobattler.scripts.module;

namespace SingleplayerAutobattler.scripts.architecture;

/// <summary>
/// 游戏架构类，负责安装和管理游戏所需的各种模块
/// 继承自AbstractArchitecture，用于构建游戏的整体架构体系
/// </summary>
public class GameArchitecture : AbstractArchitecture<GameArchitecture>
{
    /// <summary>
    /// 安装游戏所需的各个功能模块
    /// 该方法在架构初始化时被调用，用于注册系统、模型和工具模块
    /// </summary>
    protected override void InstallModules()
    {
        // 安装系统相关的Godot模块
        InstallModule(new SystemGodotModule());
        // 安装数据模型相关的Godot模块
        InstallModule(new ModelGodotModule());
        // 安装工具类相关的Godot模块
        InstallModule(new UtilityGodotModule());
        // 安装输入相关的Godot模块
        InstallGodotModule(new InputGodotModule());
    }
}