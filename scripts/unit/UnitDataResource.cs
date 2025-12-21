using Godot;
using SingleplayerAutobattler.scripts.enums;
using SingleplayerAutobattler.scripts.interfaces;

namespace SingleplayerAutobattler.scripts.unit;

/// <summary>
/// 单位数据资源类，用于存储和管理游戏单位的基本属性信息。
/// 该类继承自Godot的Resource类，可以在编辑器中作为资源文件使用。
/// 实现了ILevel接口以支持等级相关功能。
/// </summary>
[GlobalClass]
public partial class UnitDataResource : Resource, ILevel
{
    private int _level;

    /// <summary>
    /// 获取或设置单位的唯一标识符。
    /// </summary>
    [Export]
    [ExportCategory("基本属性")]
    public int Id { get; set; }
    
    /// <summary>
    /// 获取或设置单位的稀有度等级。
    /// 当新设置的值与当前值相同时，不会执行任何操作。
    /// </summary>
    [Export]
    public UnitRarity Rarity { get; set; }

    /// <summary>
    /// 获取或设置单位的名称。
    /// </summary>
    [Export]
    public string Name { get; set; }

    /// <summary>
    /// 获取或设置单位的金币消耗值。
    /// </summary>
    [Export]
    public int GoldCost { get; set; }

    /// <summary>
    /// 获取或设置单位的当前冷却消耗值。
    /// 当新设置的值与当前值相同时，不会执行任何操作。
    /// </summary>
    [Export]
    public int CurrentColdCost {get; set; }

    /// <summary>
    /// 获取或设置单位皮肤在纹理图集中的坐标位置。
    /// 使用Vector2I类型表示整数坐标。
    /// </summary>
    [Export]
    [ExportCategory("视觉")]
    public Vector2I SkinCoordinates { get; set; }

    /// <summary>
    /// 获取或设置当前等级值。取值范围为1到3。
    /// 设置时会触发资源变更通知。
    /// </summary>
    [Export(PropertyHint.Range, "1,3")]
    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            EmitChanged();
        }
    }

    /// <summary>
    /// 计算合成该单位所需的基础单位数量。
    /// 合成规则是每升一级需要3个低一级单位进行合成。
    /// 例如：1级单位返回1，2级单位返回3，3级单位返回9。
    /// </summary>
    /// <returns>合成当前等级单位所需要的基础单位总数</returns>
    public int GetCombinedUnitCount()
    {
        // 如果等级小于等于1，则只需要一个基础单位
        if (Level <= 1) return 1;
        
        // 每次升级需要3倍于上一级的数量
        var result = 1;
        for (var i = 0; i < Level - 1; i++)
        {
            result *= 3;
        }
        return result;
    }

    /// <summary>
    /// 根据单位的基础金币价值和合成数量计算总金币价值。
    /// 总价值 = 基础金币成本 × 合成所需基础单位数量。
    /// </summary>
    /// <returns>当前等级单位的总金币价值</returns>
    public int GetGoldValue()
    {
        return GoldCost * GetCombinedUnitCount();
    }
}
