using Godot;
using SingleplayerAutobattler.scripts.enums;

namespace SingleplayerAutobattler.scripts.unit;

/// <summary>
/// 单位数据资源类，用于存储和管理游戏单位的基本属性信息
/// 该类继承自Godot的Resource类，可以在编辑器中作为资源文件使用
/// </summary>
[GlobalClass]
public partial class UnitDataResource : Resource
{
    /// <summary>
    /// 获取或设置单位的唯一标识符
    /// </summary>
    [Export]
    [ExportCategory("基本属性")]
    public int Id { get; set; }
    
    /// <summary>
    /// 获取或设置单位的稀有度等级
    /// 当新设置的值与当前值相同时，不会执行任何操作
    /// </summary>
    [Export]
    public UnitRarity Rarity { get; set; }

    /// <summary>
    /// 获取或设置单位的名称
    /// </summary>
    [Export]
    public string Name { get; set; }

    /// <summary>
    /// 获取或设置单位的金币消耗值
    /// </summary>
    [Export]
    public int GoldCost { get; set; }

    /// <summary>
    /// 获取或设置单位的当前冷却消耗值
    /// 当新设置的值与当前值相同时，不会执行任何操作
    /// </summary>
    [Export]
    public int CurrentColdCost {get; set; }

    /// <summary>
    /// 获取或设置单位皮肤在纹理图集中的坐标位置
    /// 使用Vector2I类型表示整数坐标
    /// </summary>
    [Export]
    [ExportCategory("视觉")]
    public Vector2I SkinCoordinates { get; set; }
}
