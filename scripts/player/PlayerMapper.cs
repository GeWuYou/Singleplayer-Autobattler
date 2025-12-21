
using GFramework.Core.utility;
using Riok.Mapperly.Abstractions;

namespace SingleplayerAutobattler.scripts.player;

/// <summary>
/// PlayerMapper类用于在PlayerData和PlayerDataResource之间进行映射转换。
/// 该类使用Mapperly库自动生成映射代码，提供高性能的对象映射功能。
/// </summary>
[Mapper]
public partial class PlayerMapper : IUtility
{
    /// <summary>
    /// 将PlayerDataResource对象映射转换为PlayerData对象。
    /// 该方法由Mapperly库自动生成实现，用于将资源数据转换为游戏数据。
    /// </summary>
    /// <param name="resource">要转换的PlayerDataResource对象，包含玩家的原始资源数据</param>
    /// <returns>转换后的PlayerData对象，包含游戏逻辑中使用的玩家数据</returns>
    public partial PlayerData DataFromResource(PlayerDataResource? resource);

    /// <summary>
    /// 将PlayerData对象映射转换为PlayerDataResource对象。
    /// 该方法由Mapperly库自动生成实现，用于将游戏数据转换为资源数据。
    /// </summary>
    /// <param name="data">要转换的PlayerData对象，包含游戏逻辑中的玩家数据</param>
    /// <returns>转换后的PlayerDataResource对象，包含可序列化的玩家资源数据</returns>
    public partial PlayerDataResource ResourceFromData(PlayerData? data);
}