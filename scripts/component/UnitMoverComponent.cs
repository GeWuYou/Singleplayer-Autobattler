using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Core.extensions;
using Godot;
using Godot.Collections;
using SingleplayerAutobattler.scripts.architecture;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.component;

/// <summary>
/// 单位移动组件，用于处理单位在不同玩家区域之间的移动逻辑。
/// 实现了IController接口以接入游戏架构系统。
/// </summary>
public partial class UnitMoverComponent : Node, IController
{
    [Export] public Array<PlayerAreaComponent>? PlayerAreas;

    /// <summary>
    /// 根据全局坐标获取对应的游戏区域索引
    /// </summary>
    /// <param name="global">全局坐标位置</param>
    /// <returns>返回对应玩家区域的索引，如果未找到则返回-1</returns>
    private int GetPlayAreaForPosition(Vector2 global)
    {
        // 遍历所有玩家区域，查找包含指定坐标的区域
        var index = -1;
        for (var i = 0; i < PlayerAreas!.Count; i++)
        {
            var playerArea = PlayerAreas[i];
            var tile = playerArea.GetTileFromGlobal(global);
            if (playerArea.IsTileInBounds(tile))
            {
                index = i;
            }
        }

        return index;
    }

    /// <summary>
    /// 将单位重置到起始位置，并更新其所在区域的网格信息
    /// </summary>
    /// <param name="startingPosition">单位的起始全局坐标位置</param>
    /// <param name="unit">需要被重置的单位实例</param>
    private void ResetUnitToStartingPosition(Vector2 startingPosition, Unit unit)
    {
        // 获取指定位置所属的游戏区域索引
        var i = GetPlayAreaForPosition(startingPosition);

        // 将全局坐标转换为区域内的格子坐标
        var tile = PlayerAreas![i].GetTileFromGlobal(startingPosition);

        // 重置单位状态并更新网格信息
        unit.RestAfterDragging(startingPosition);
        PlayerAreas[i].UnitGrid!.AddUnit(tile, unit);
    }

    /// <summary>
    /// 移动单位到指定格子位置，并更新单位的父节点和全局坐标
    /// </summary>
    /// <param name="unit">需要移动的单位实例</param>
    /// <param name="playerAreaComponent">目标玩家区域组件</param>
    /// <param name="tile">目标格子坐标</param>
    private static void MoveUnit(Unit unit, PlayerAreaComponent playerAreaComponent, Vector2I tile)
    {
        // 在目标区域网格中添加单位引用
        playerAreaComponent.UnitGrid!.AddUnit(tile, unit);
        // 更新单位的全局位置并重新设置父节点
        unit.GlobalPosition = playerAreaComponent.GetGlobalFromTile(tile) - ArenaConstants.HalfCellSizeVector;
        unit.Reparent(playerAreaComponent);
    }

    /// <summary>
    /// 获取游戏架构实例
    /// </summary>
    /// <returns>返回游戏架构接口实例</returns>
    public IArchitecture GetArchitecture() => GameArchitecture.Instance;
    
    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        var units = GetTree().GetNodesInGroup(ArenaConstants.UnitsName);
        foreach (var unit in units)
        {
            SetupUnit(unit as Unit);
        }
    }

    /// <summary>
    /// 启用或禁用所有玩家区域中的高亮显示功能
    /// </summary>
    /// <param name="enable">是否启用高亮显示</param>
    private void SetHighlighters(bool enable)
    {
        if (PlayerAreas == null) return;
        foreach (var playerAreaComponent in PlayerAreas)
        {
            playerAreaComponent.TileHighlighter!.Enable = enable;
        }
    }

    /// <summary>
    /// 设置单位拖拽事件监听器
    /// </summary>
    /// <param name="unit">要绑定事件的单位对象</param>
    private void SetupUnit(Unit? unit)
    {
        unit!.DragDropComponent!.Connect(DragDropComponent.SignalName.DragStarted,Callable.From(()=> OnDragStarted(unit)));
        unit.DragDropComponent!.Connect(DragDropComponent.SignalName.DragCanceled,
            Callable.From<Vector2>(startingPosition=> OnDragCanceled(startingPosition,unit)));
        unit.DragDropComponent!.Connect(DragDropComponent.SignalName.Dropped,
            Callable.From<Vector2>(startingPosition=> OnDropped(startingPosition,unit)));
    }

    /// <summary>
    /// 处理单位放置完成后的逻辑：包括跨区移动、交换位置等操作
    /// </summary>
    /// <param name="startingPosition">单位开始拖拽前的位置</param>
    /// <param name="unit">当前正在操作的单位</param>
    private void OnDropped(Vector2 startingPosition, Unit unit)
    {
        // 关闭高亮显示
        SetHighlighters(false);
    
        // 获取起始位置和鼠标位置对应的区域索引
        var oldAreaIndex = GetPlayAreaForPosition(startingPosition);
        var newAreaIndex = GetPlayAreaForPosition(unit.GetGlobalMousePosition());
    
        // 如果新位置不在有效区域内，则重置单位到起始位置
        if (newAreaIndex == -1)
        {
            ResetUnitToStartingPosition(startingPosition, unit);
            return;
        }
    
        // 获取起始区域和目标区域的引用
        var oldArea = PlayerAreas![oldAreaIndex];
        var oldTile = oldArea.GetTileFromGlobal(startingPosition);
        var newArea = PlayerAreas![newAreaIndex];
        var newTile = newArea.GetHoveredTile();
    
        // 检查目标位置是否已被占用，如果占用则先移动占位单位
        if (newArea.UnitGrid!.IsTileOccupied(newTile))
        {
            var oldUnit = newArea.UnitGrid.UnitsDictionary[newTile];
            newArea.UnitGrid.RemoveUnit(newTile);
            MoveUnit(oldUnit!, oldArea, oldTile);
        }
    
        // 执行单位移动到新位置
        MoveUnit(unit, newArea, newTile);
    }

    /// <summary>
    /// 当用户取消拖拽动作时，将单位恢复至原始位置
    /// </summary>
    /// <param name="startingPosition">单位开始拖拽前的位置</param>
    /// <param name="unit">当前正在操作的单位</param>
    private void OnDragCanceled(Vector2 startingPosition, Unit unit)
    {
        SetHighlighters(false);
        ResetUnitToStartingPosition(startingPosition, unit);
    }

    /// <summary>
    /// 开始拖拽单位时触发的操作，移除该单位在其原属区域网格上的记录
    /// </summary>
    /// <param name="unit">当前正在操作的单位</param>
    private void OnDragStarted(Unit unit)
    {
        SetHighlighters(true);
        var i = GetPlayAreaForPosition(unit.GlobalPosition);
        if (i <= -1) return;
        var tile = PlayerAreas![i].GetTileFromGlobal(unit.GlobalPosition);
        PlayerAreas![i].UnitGrid!.RemoveUnit(tile);
    }
}
