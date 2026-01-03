using GFramework.Core.command;
using GFramework.Core.extensions;
using SingleplayerAutobattler.scripts.player;
using SingleplayerAutobattler.scripts.shop;
using SingleplayerAutobattler.scripts.system;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.command;

/// <summary>
/// 表示购买单位的命令结构体
/// </summary>
/// <remarks>
/// 该结构体用于封装购买单位操作所需的数据，包含要购买的单位数据资源信息
/// </remarks>
public class BuyUnitCommand(UnitDataResource unit) : AbstractCommand<bool>
{
    /// <summary>
    /// 执行购买单位命令
    /// </summary>
    /// <remarks>
    /// 该方法执行购买单位的核心逻辑，包括验证购买条件、扣除金币和标记已购买状态
    /// </remarks>
    protected override bool OnExecute()
    {
        // 获取商店系统实例
        var shopSystem = this.GetSystem<IShopSystem>()!;
        
        // 获取玩家模型和商店模型实例
        var playerModel = this.GetModel<IPlayerModel>()!;
        var shopModel = this.GetModel<IShopModel>()!;
        
        // 验证是否可以购买单位，如果不能购买则直接返回
        if (shopSystem.CanBuyUnit(playerModel.PlayerDataResource,unit)) return false;
        // 验证单位是否已经购买过，如果已购买则直接返回
        if (shopModel.IsBought(unit)) return false;
        // 从玩家金币中扣除购买单位所需的金币
        playerModel.ChangeGold(-unit.GoldCost);
        // 在商店模型中标记该单位为已购买状态
        shopModel.MarkBought(unit);
        return true;
    }
}
