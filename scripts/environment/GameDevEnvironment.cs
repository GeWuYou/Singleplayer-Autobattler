using GFramework.Core.environment;
using SingleplayerAutobattler.global;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.environment;

/// <summary>
/// 游戏开发环境类，继承自EnvironmentBase
/// 用于管理开发环境下的特定资源和配置
/// </summary>
public class GameDevEnvironment: EnvironmentBase
{
    /// <summary>
    /// 获取或设置玩家数据资源
    /// </summary>
    public PlayerDataResource PlayerDataResource { get; set; } = null!;
    
    /// <summary>
    /// 初始化开发环境
    /// 从开发绑定管理器中获取玩家数据资源并进行初始化
    /// </summary>
    public override void Initialize()
    {
        PlayerDataResource = DevBindingManager.Instance.PlayerDataResource;
    }

    /// <summary>
    /// 获取环境名称
    /// </summary>
    public override string Name { get; } = GameConstants.Development;
}
