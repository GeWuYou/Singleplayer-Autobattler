using Godot;
using SingleplayerAutobattler.scripts.data;
using SingleplayerAutobattler.scripts.enums;
using SingleplayerAutobattler.scripts.interfaces;

namespace SingleplayerAutobattler.scripts.unit;

/// <summary>
/// UnitData类表示游戏单位的数据结构，继承自BaseData基类。
/// 该类存储了单位的基本属性信息，包括稀有度、名称、成本和皮肤坐标等。
/// </summary>
public class UnitData: BaseData,ILevel
{
    /// <summary>
    /// 获取或设置单位的稀有度等级
    /// </summary>
    public UnitRarity Rarity { get; set; }
    
    /// <summary>
    /// 获取或设置单位的名称
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 获取或设置单位的金币消耗值
    /// </summary>
    public int GoldCost { get; set; }
    
    /// <summary>
    /// 获取或设置单位当前的冷却消耗值
    /// </summary>
    public int CurrentColdCost { get; set; }
    
    /// <summary>
    /// 获取或设置单位皮肤在纹理图集中的坐标位置
    /// </summary>
    public Vector2I SkinCoordinates { get; set; }

    /// <summary>
    /// 获取或设置当前等级值
    /// </summary>
    public int Level { get; set; }
}
