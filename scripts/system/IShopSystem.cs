using GFramework.Core.Abstractions.system;
using SingleplayerAutobattler.scripts.player;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.system;

/// <summary>
/// 商店系统接口，定义了游戏商店系统的功能契约
/// </summary>
public interface IShopSystem: ISystem
{
    /// <summary>
    /// 判断玩家是否能够购买指定单位
    /// </summary>
    /// <param name="playerData">玩家数据资源，包含玩家的金币、等级等信息</param>
    /// <param name="unitData">要购买的单位数据资源，包含单位的价格、属性等信息</param>
    /// <returns>如果能够购买则返回true，否则返回false</returns>
    public bool CanBuyUnit(PlayerDataResource playerData,UnitDataResource unitData);
}
