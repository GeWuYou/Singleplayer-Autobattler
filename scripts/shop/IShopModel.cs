using GFramework.Core.Abstractions.model;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.shop;

/// <summary>
/// 商店模型接口，定义了商店相关的购买状态管理功能
/// </summary>
public interface IShopModel : IModel
{
    /// <summary>
    /// 检查指定单位是否已被购买
    /// </summary>
    /// <param name="unit">要检查的单位数据资源</param>
    /// <returns>如果单位已被购买则返回true，否则返回false</returns>
    bool IsBought(UnitDataResource unit);
    
    /// <summary>
    /// 标记指定单位为已购买状态
    /// </summary>
    /// <param name="unit">要标记为已购买的单位数据资源</param>
    void MarkBought(UnitDataResource unit);
}