using Godot;

namespace SingleplayerAutobattler.scripts.constants;

/// <summary>
/// 存储竞技场相关的常量数据类
/// 提供单元格大小及其派生尺寸的向量表示
/// </summary>
public static class ArenaConstants
{
    /// <summary>
    /// arenas子节点名称
    /// </summary>
    public const string UnitsName = "Units";
    /// <summary>
    /// 单元格基础大小（像素）
    /// </summary>
    private const int CellSize = 32;
    
    /// <summary>
    /// 半个单元格大小（像素）
    /// </summary>
    private const int HalfCellSize = CellSize / 2;
    
    /// <summary>
    /// 四分之一个单元格大小（像素）
    /// </summary>
    private const int QuarterCellSize = CellSize / 4;
    
    /// <summary>
    /// 单元格大小的向量表示
    /// </summary>
    public static readonly Vector2 CellSizeVector = new(CellSize, CellSize);
    
    /// <summary>
    /// 半个单元格大小的向量表示
    /// </summary>
    public static readonly Vector2 HalfCellSizeVector = new(HalfCellSize, HalfCellSize);
    
    /// <summary>
    /// 四分之一个单元格大小的向量表示
    /// </summary>
    public static readonly Vector2 QuarterCellSizeVector = new(QuarterCellSize, QuarterCellSize);
}

