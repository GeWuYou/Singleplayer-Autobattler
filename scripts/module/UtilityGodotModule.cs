using GFramework.Core.architecture;
using GFramework.Godot.architecture;
using Godot;
using SingleplayerAutobattler.scripts.architecture;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.module;



/// <summary>
/// Godot工具模块类，负责安装和管理游戏中的实用工具组件
/// </summary>
public class UtilityGodotModule: AbstractGodotModule<GameArchitecture>
{
    /// <summary>
    /// 安装模块到指定的游戏架构中
    /// </summary>
    /// <param name="architecture">要安装模块的目标游戏架构实例</param>
    public override void Install(IArchitecture architecture)
    {
        // 注册单位映射器实用程序到架构中
        architecture.RegisterUtility(new UnitMapper());
    }

    /// <summary>
    /// 获取模块关联的Godot节点
    /// </summary>
    /// <returns>关联的Godot节点实例</returns>
    public override Node Node { get; }
}


