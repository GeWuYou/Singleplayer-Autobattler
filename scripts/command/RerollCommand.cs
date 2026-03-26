using SingleplayerAutobattler.scripts.player;
using SingleplayerAutobattler.scripts.system;

namespace SingleplayerAutobattler.scripts.command;

/// <summary>
///     重-roll命令类，用于处理玩家的重-roll操作
/// </summary>
/// <param name="input">重-roll命令输入参数</param>
public class RerollCommand(RerollCommandInput input) : AbstractCommand<RerollCommandInput>(input)
{
    /// <summary>
    ///     执行重-roll命令的逻辑
    /// </summary>
    /// <param name="_">重-roll命令输入参数（未使用）</param>
    /// <remarks>
    ///     该方法获取玩家模型和重-roll系统，检查是否可以进行重-roll操作，
    ///     如果可以则扣除相应的金币费用
    /// </remarks>
    protected override void OnExecute(RerollCommandInput _)
    {
        // 获取玩家模型
        var player = this.GetModel<IPlayerModel>()!;

        // 获取重-roll系统
        var rerollSystem = this.GetSystem<IRerollSystem>()!;

        // 检查是否可以进行重-roll操作
        if (!rerollSystem.CanReroll(player.PlayerDataResource)) return;

        // 扣除重-roll所需的金币
        player.ChangeGold(-rerollSystem.GetRerollCost());
    }
}

/// <summary>
///     重-roll命令输入类
/// </summary>
public sealed class RerollCommandInput : ICommandInput;