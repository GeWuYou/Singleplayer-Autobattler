using GFramework.Core.Command;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.command;

/// <summary>
///     购买经验值命令类，用于处理玩家购买经验值的业务逻辑
/// </summary>
/// <param name="input">购买经验值命令输入参数</param>
public class BuyXpCommand(BuyXpCommandInput input) : AbstractCommand<BuyXpCommandInput>(input)
{
    /// <summary>
    ///     执行购买经验值命令的业务逻辑
    /// </summary>
    /// <param name="input">购买经验值命令输入参数，包含经验值数量和金币消耗</param>
    protected override void OnExecute(BuyXpCommandInput input)
    {
        // 获取玩家模型实例
        var player = this.GetModel<IPlayerModel>()!;
        // 给玩家增加经验值
        player.GainXp(input.XpAmount);
        // 扣除玩家金币
        player.ChangeGold(-input.GoldCost);
    }
}

/// <summary>
///     购买经验值命令输入类，包含购买经验值所需的数据参数
/// </summary>
public sealed class BuyXpCommandInput : ICommandInput
{
    /// <summary>
    ///     购买的经验值数量
    /// </summary>
    public int XpAmount { get; init; }

    /// <summary>
    ///     购买所需消耗的金币数量
    /// </summary>
    public int GoldCost { get; init; }
}