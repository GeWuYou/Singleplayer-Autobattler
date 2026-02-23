using GFramework.Core.Abstractions.system;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.system;

/// <summary>
///     重-roll系统接口，定义了与重-roll功能相关的操作
/// </summary>
public interface IRerollSystem : ISystem
{
    /// <summary>
    ///     判断指定玩家是否可以进行重-roll操作
    /// </summary>
    /// <param name="playerData">玩家数据资源对象</param>
    /// <returns>如果玩家可以重-roll则返回true，否则返回false</returns>
    public bool CanReroll(PlayerDataResource playerData);

    /// <summary>
    ///     获取当前重-roll操作所需的费用
    /// </summary>
    /// <returns>重-roll操作的成本值</returns>
    public int GetRerollCost();
}