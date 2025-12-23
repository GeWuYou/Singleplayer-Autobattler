using GFramework.Core.architecture;
using GFramework.Core.logging;
using GFramework.Godot.architecture;
using GFramework.Godot.logging;
using Godot;
using SingleplayerAutobattler.scripts.architecture;

namespace SingleplayerAutobattler.scripts.module;

/// <summary>
/// Godot日志模块类，负责初始化和配置Godot平台的日志系统
/// 用于游戏架构的日志功能模块
/// </summary>
public class LoggerGodotModule: AbstractGodotModule<GameArchitecture>
{
    /// <summary>
    /// 安装并配置日志模块
    /// </summary>
    /// <param name="architecture">要安装模块的架构实例</param>
    public override void Install(IArchitecture architecture)
    {
        // 设置Godot日志记录器实例
        Log.SetLogger(new GodotLogger());
        
        // 配置日志系统参数
        Log.Configure(
            minLevel: LogLevel.Debug,        // 设置最小日志级别为Debug
            enableConsole: true,             // 启用控制台输出
            useColors: true                  // 启用彩色输出
        );
        
        // 设置日志记录器工厂
        Log.SetLoggerFactory(new LoggerFactory(Log.Config));
    }

    /// <summary>
    /// 获取模块关联的Godot节点
    /// </summary>
    public override Node Node { get; }
}
