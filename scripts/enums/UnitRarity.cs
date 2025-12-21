using GFramework.Generator.Attributes.generator.enums;

namespace SingleplayerAutobattler.scripts.enums;

/// <summary>
/// 单位稀有度枚举，定义了游戏中单位的不同稀有度等级
/// 稀有度从低到高依次为：普通、罕见、稀有、史诗、传奇、神话
/// </summary>
[GenerateEnumExtensions]
public enum UnitRarity
{
    /// <summary>
    /// 普通稀有度 - 最基础的单位稀有度
    /// </summary>
    Common,
    
    /// <summary>
    /// 罕见稀有度 - 比普通稀有度更高一级
    /// </summary>
    Uncommon,
    
    /// <summary>
    /// 稀有稀有度 - 较为珍贵的单位稀有度
    /// </summary>
    Rare,
    
    /// <summary>
    /// 史诗稀有度 - 非常强力且稀有的单位
    /// </summary>
    Epic,
    
    /// <summary>
    /// 传奇稀有度 - 极其罕见且强大的单位
    /// </summary>
    Legendary,
    
    /// <summary>
    /// 神话稀有度 - 最高等级的单位稀有度
    /// </summary>
    Mythic
}
