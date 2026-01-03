using GFramework.Core.system;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.system.impl;

/// <summary>
/// 重-roll系统类，负责处理游戏中的重-roll功能
/// </summary>
public class RerollSystem: AbstractSystem, IRerollSystem
{
    /// <summary>
    /// 重-roll操作所需的金币成本
    /// </summary>
    private const int RerollCost = 2;
    
    protected override void OnInit()
    {
        
    }

    /// <summary>
    /// 检查玩家是否有足够的金币进行重-roll操作
    /// </summary>
    /// <param name="playerData">玩家数据资源对象</param>
    /// <returns>如果玩家金币足够则返回true，否则返回false</returns>
    public bool CanReroll(PlayerDataResource playerData)
    {
        // 检查玩家金币是否大于等于重-roll成本
        return playerData.Gold >= RerollCost;
    }

    public int GetRerollCost()
    {
        return RerollCost;
    }
}
