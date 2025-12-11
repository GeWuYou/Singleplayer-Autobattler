using GFramework.Core.utility;
using Riok.Mapperly.Abstractions;

namespace SingleplayerAutobattler.scripts.unit;
/// <summary>
/// UnitMapper类用于在UnitData和UnitDataResource之间进行映射转换。
/// 该类使用Mapperly库自动生成映射代码，提供高性能的对象映射功能。
/// </summary>
[Mapper]
public partial class UnitMapper: IUtility
{
    /// <summary>
    /// 将UnitDataResource对象映射转换为UnitData对象。
    /// 该方法由Mapperly库自动生成实现，用于将资源数据转换为游戏单位数据。
    /// </summary>
    /// <param name="resource">要转换的UnitDataResource对象，包含单位的原始资源数据</param>
    /// <returns>转换后的UnitData对象，包含游戏逻辑中使用的单位数据</returns>
    public partial UnitData DataFromResource(UnitDataResource resource);
}
