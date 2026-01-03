using System.Collections.Generic;
using GFramework.Core.model;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.shop;

/// <summary>
/// 商店模型类，用于管理商店中单位的购买状态
/// </summary>
public class ShopModel : AbstractModel, IShopModel
{
    /// <summary>
    /// 存储已购买单位数据资源的哈希集合
    /// </summary>
    private readonly HashSet<int> _bought = [];

    /// <summary>
    /// 检查指定单位是否已被购买
    /// </summary>
    /// <param name="unit">要检查的单位数据资源</param>
    /// <returns>如果单位已被购买则返回true，否则返回false</returns>
    public bool IsBought(UnitDataResource unit)
        => _bought.Contains(unit.Id);

    /// <summary>
    /// 标记指定单位为已购买状态
    /// </summary>
    /// <param name="unit">要标记为已购买的单位数据资源</param>
    public void MarkBought(UnitDataResource unit)
        => _bought.Add(unit.Id);

    /// <summary>
    /// 初始化商店模型
    /// </summary>
    protected override void OnInit()
    {
        
    }
}

