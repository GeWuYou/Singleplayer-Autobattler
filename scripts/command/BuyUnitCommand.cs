using GFramework.Core.Abstractions.command;
using GFramework.Core.command;
using GFramework.Core.extensions;
using GFramework.SourceGenerators.Abstractions.logging;
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
[Log]
public partial class BuyUnitCommand(BuyUnitCommandInput input) : AbstractCommand<BuyUnitCommandInput, bool>(input)
{
    /// <summary>
    /// 执行购买单位命令
    /// </summary>
    /// <param name="input">购买单位命令输入参数，包含要购买的单位数据资源</param>
    /// <returns>返回购买操作是否成功，true表示购买成功，false表示购买失败</returns>
    /// <remarks>
    /// 该方法执行购买单位的核心逻辑，包括验证购买条件、扣除金币和标记已购买状态
    /// </remarks>
    protected override bool OnExecute(BuyUnitCommandInput input)
    {
        var unit = input.UnitDataResource;
        // 获取商店系统实例
        var shopSystem = this.GetSystem<IShopSystem>()!;

        // 获取玩家模型和商店模型实例
        var playerModel = this.GetModel<IPlayerModel>()!;
        var shopModel = this.GetModel<IShopModel>()!;
        // 验证是否可以购买单位，如果不能购买则直接返回
        if (!shopSystem.CanBuyUnit(playerModel.PlayerDataResource, unit)) return false;
        // 验证单位是否已经购买过，如果已购买则直接返回
        if (shopModel.IsBought(unit)) return false;
        _log.Debug("isBought {0} canBuyUnit {1}", shopModel.IsBought(unit),
            shopSystem.CanBuyUnit(playerModel.PlayerDataResource, unit));
        // 在商店模型中标记该单位为已购买状态
        shopModel.MarkBought(unit);
        // 从玩家金币中扣除购买单位所需的金币
        playerModel.ChangeGold(-unit.GoldCost);
        return true;
    }
}

/// <summary>
/// 购买单位命令输入类
/// </summary>
/// <remarks>
/// 该类用于封装购买单位命令所需的输入数据
/// </remarks>
public sealed class BuyUnitCommandInput : ICommandInput
{
    /// <summary>
    /// 要购买的单位数据资源
    /// </summary>
    public UnitDataResource UnitDataResource { get; init; } = null!;
}
