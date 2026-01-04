using System.Collections.Generic;
using Godot;

namespace SingleplayerAutobattler.scripts.player;

/// <summary>
/// 玩家数据资源类，用于存储和管理玩家的游戏数据
/// 继承自Godot的Resource类，可作为游戏资源配置使用
/// </summary>
[GlobalClass]
public partial class PlayerDataResource : Resource
{
    private int _gold;
    private int _xp;
    private int _level;

    public static int MaxLevel => XpTable.Count;

    /// <summary>
    /// 经验值表，存储等级与所需经验值的映射关系
    /// </summary>
    private static readonly Dictionary<int, int> XpTable = new()
    {
        { 1, 0 },
        { 2, 5 },
        { 3, 7 },
        { 4, 10 },
        { 5, 15 },
        { 6, 20 }
    };

    /// <summary>
    /// 获取当前等级升级所需的经验值
    /// </summary>
    /// <returns>升级到下一级所需的经验值</returns>
    public int GetCurrentXpRequirement()
    {
        var nextLevel = Mathf.Clamp(Level + 1, 1, MaxLevel);
        return XpTable[nextLevel];
    }


    /// <summary>
    /// 玩家拥有的金币数量
    /// 取值范围：0-99
    /// </summary>
    [Export(PropertyHint.Range, "0,99")]
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            // 当金币数值发生变化时，触发资源变更事件
            EmitChanged();
        }
    }

    /// <summary>
    /// 玩家当前经验值
    /// 取值范围：0-99
    /// </summary>
    [Export(PropertyHint.Range, "0,99")]
    public int Xp
    {
        get => _xp;
        set
        {
            _xp = Mathf.Max(0, value);
            TryLevelUp();
            EmitChanged();
        }
    }

    /// <summary>
    /// 玩家等级
    /// 取值范围：1-6
    /// </summary>
    [Export(PropertyHint.Range, "1,6")]
    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            // 当等级发生变化时，触发资源变更事件
            EmitChanged();
        }
    }

    private void TryLevelUp()
    {
        if (_level >= MaxLevel)
            return;
        var xpRequirement = GetCurrentXpRequirement();
        while (_level < MaxLevel && Xp >= xpRequirement)
        {
            _level++;
            _xp -= xpRequirement;
            xpRequirement = GetCurrentXpRequirement();
            EmitChanged();
        }
    }
}