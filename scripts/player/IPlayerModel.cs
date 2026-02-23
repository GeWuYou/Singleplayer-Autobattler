using GFramework.Core.Abstractions.model;

namespace SingleplayerAutobattler.scripts.player;

/// <summary>
///     玩家模型接口，定义了玩家数据和相关操作的契约
/// </summary>
public interface IPlayerModel : IModel
{
    /// <summary>
    ///     获取或设置玩家数据资源
    /// </summary>
    public PlayerDataResource PlayerDataResource { get; set; }

    /// <summary>
    ///     修改玩家金币数量
    /// </summary>
    /// <param name="value">金币变化值，正数为增加，负数为减少</param>
    /// <returns>修改后的金币总数</returns>
    public int ChangeGold(int value);

    /// <summary>
    ///     玩家获得经验值
    /// </summary>
    /// <param name="amount">获得的经验值数量</param>
    void GainXp(int amount);

    /// <summary>
    ///     检查玩家是否有足够的金币
    /// </summary>
    /// <param name="cost">需要检查的金币消耗值</param>
    /// <returns>如果金币足够则返回true，否则返回false</returns>
    bool HasEnoughGold(int cost);


    /// <summary>
    ///     检查玩家是否达到最高等级
    /// </summary>
    /// <returns>是否达到最高等级</returns>
    bool IsMaxLevel();
}