namespace SingleplayerAutobattler.scripts.data;

/// <summary>
/// 基础数据类，提供所有数据对象的基类实现
/// 包含一个唯一的标识符属性，用于区分不同的数据实例
/// </summary>
public class BaseData
{
    /// <summary>
    /// 获取或设置数据对象的唯一标识符
    /// </summary>
    /// <value>表示数据对象唯一性的整数值</value>
    public int Id { get; set; }
}