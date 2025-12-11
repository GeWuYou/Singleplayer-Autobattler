using System.Collections.Generic;
using Godot;
using SingleplayerAutobattler.scripts.enums;

namespace SingleplayerAutobattler.scripts.constants;

/// <summary>
/// UnitRarityColors
/// </summary>
public static class UnitRarityColors
{
    /// <summary>
    /// 稀有度对应的颜色列表，索引与UnitRarity枚举值对应
    /// </summary>
    public static readonly IReadOnlyList<Color> RarityColors = new List<Color>
    {
        // Common - 灰色
        new(0.8f, 0.8f, 0.8f),
        // Uncommon - 绿色
        new(0.2f, 0.8f, 0.2f),
        // Rare - 蓝色
        new(0.2f, 0.4f, 1.0f),
        // Epic - 紫色
        new(0.6f, 0.2f, 0.8f),
        // Legendary - 橙色
        new(1.0f, 0.5f, 0.0f),
        // Mythic - 红色
        new(1.0f, 0.1f, 0.1f)
    };

    /// <summary>
    /// 获取指定稀有度的颜色
    /// </summary>
    /// <param name="rarity">单位稀有度</param>
    /// <returns>对应的Color对象</returns>
    public static Color GetColor(this UnitRarity rarity)
    {
        return RarityColors[(int)rarity];
    }
}