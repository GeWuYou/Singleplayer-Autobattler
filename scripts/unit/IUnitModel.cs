using System.Collections.Generic;
using System.Threading.Tasks;
using GFramework.Core.model;

namespace SingleplayerAutobattler.scripts.unit;

/// <summary>
/// 单位模型接口，继承自IModel接口
/// 定义了单位数据字典的访问接口
/// </summary>
public interface IUnitModel: IModel
{
    /// <summary>
    /// 获取或设置单位数据字典
    /// 字典键为单位ID，值为对应的单位数据对象
    /// </summary>
    public Dictionary<int, UnitData> UnitDictionary { get; set; }
}
