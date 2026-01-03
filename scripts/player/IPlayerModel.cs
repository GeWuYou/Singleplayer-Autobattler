using GFramework.Core.Abstractions.model;

namespace SingleplayerAutobattler.scripts.player;

/// <summary>
/// 玩家模型接口，定义了玩家数据和相关操作的契约
/// </summary>
public interface IPlayerModel: IModel
{
    /// <summary>
    /// 获取或设置玩家数据资源
    /// </summary>
    public PlayerDataResource PlayerDataResource { get; set; }
    
    /// <summary>
    /// 修改玩家金币数量
    /// </summary>
    /// <param name="value">金币变化值，正数为增加，负数为减少</param>
    /// <returns>修改后的金币总数</returns>
    public int ChangeGold(int value);
}
