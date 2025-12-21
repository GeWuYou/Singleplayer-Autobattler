namespace SingleplayerAutobattler.scripts.interfaces;

/// <summary>
/// 定义等级相关的接口，用于表示具有等级属性的游戏对象
/// </summary>
public interface ILevel
{
    /// <summary>
    /// 获取或设置当前等级值
    /// </summary>
    public int Level { get; set; }
}
