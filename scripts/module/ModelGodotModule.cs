using GFramework.Core.architecture;
using GFramework.Godot.architecture;
using Godot;
using SingleplayerAutobattler.scripts.architecture;
using SingleplayerAutobattler.scripts.arena;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.module;

/// <summary>
/// Godot模块实现类，负责安装和注册游戏中的各种模型
/// </summary>
public class ModelGodotModule: AbstractGodotModule<GameArchitecture>
{
    /// <summary>
    /// 安装模块方法，向架构中注册ArenaModel和UnitModel模型
    /// </summary>
    /// <param name="architecture">游戏架构实例，用于注册模型</param>
    public override void Install(IArchitecture architecture)
    {
        // 注册竞技场模型
        architecture.RegisterModel(new ArenaModel());
        // 注册单位模型
        architecture.RegisterModel(new UnitModel());
    }

    /// <summary>
    /// 获取模块对应的节点对象
    /// </summary>
    public override Node Node { get; }
}
