using System.Linq;
using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Core.extensions;
using GFramework.Core.Godot.extensions;
using Godot;
using Godot.Collections;
using SingleplayerAutobattler.scripts.architecture;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.component;

/// <summary>
/// 表示一个单位网格组件，用于管理单位在二维网格上的布局。
/// 实现了 IController 接口以支持框架中的控制器模式。
/// 提供了添加、查询单位以及判断格子占用状态等功能。
/// </summary>
public partial class UnitGridComponent : Node, IController
{
    [Signal]
    public delegate void UnitGridChangedEventHandler();

    /// <summary>
    /// 网格尺寸（宽度和高度）
    /// </summary>
    [Export] public Vector2I GridSize { get; set; }

    /// <summary>
    /// 存储每个网格位置与其对应单位的映射字典
    /// </summary>
    [Export] public Dictionary<Vector2I, Unit?> UnitsDictionary { get; set; }

    /// <summary>
    /// 获取游戏架构实例
    /// </summary>
    /// <returns>返回游戏架构接口实例</returns>
    public IArchitecture GetArchitecture() => GameArchitecture.Instance;

    private UnitModel? _unitModel;
    private UnitMapper? _unitMapper;

    /// <summary>
    /// 将指定单位添加到给定的网格坐标上，并更新模型数据
    /// </summary>
    /// <param name="tile">要放置单位的网格坐标</param>
    /// <param name="unit">要添加的单位对象</param>
    public void AddUnit(Vector2I tile, Unit unit)
    {
        UnitsDictionary[tile] = unit;
        _unitModel!.UnitDictionary[tile] = _unitMapper!.DataFromResource(unit.UnitDataResource);
        EmitSignalUnitGridChanged();
    }

    /// <summary>
    /// 判断指定网格是否已被单位占据
    /// </summary>
    /// <param name="tile">需要检查的网格坐标</param>
    /// <returns>如果该位置有有效单位则返回 true，否则返回 false</returns>
    public bool IsTileOccupied(Vector2I tile)
    {
        return UnitsDictionary[tile].IsValidNode();
    }

    /// <summary>
    /// 检查整个网格是否已满（所有格子都被占用）
    /// </summary>
    /// <returns>如果所有格子都已被占用则返回 true，否则返回 false</returns>
    public bool IsGridFull()
    {
        return UnitsDictionary.Keys.All(IsTileOccupied);
    }

    /// <summary>
    /// 查找并返回第一个未被占用的网格坐标
    /// </summary>
    /// <returns>第一个空闲的网格坐标；如果没有找到，则返回 (-1, -1)</returns>
    public Vector2I GetFirstEmptyTile()
    {
        return UnitsDictionary.Keys.FirstOrDefault(tile => !IsTileOccupied(tile), new Vector2I(-1, -1));
    }

    /// <summary>
    /// 获取当前网格中所有的有效单位列表
    /// </summary>
    /// <returns>包含所有有效单位的数组</returns>
    public Array<Unit> GetAllUnits()
    {
        return new Array<Unit>(UnitsDictionary.Values.Where(unit => unit.IsValidNode()).ToArray()!);
    }

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// 初始化网格结构并将初始值设为空
    /// </summary>
    public override void _Ready()
    {
        _unitModel = this.GetModel<UnitModel>();
        _unitMapper = this.GetUtility<UnitMapper>();

        // 遍历网格大小初始化字典键值对
        for (var i = 0; i < GridSize.X; i++)
        {
            for (var j = 0; j < GridSize.Y; j++)
            {
                var key = new Vector2I(i, j);
                UnitsDictionary[key] = null;
                _unitModel.UnitDictionary[key] = null;
            }
        }
    }
}
