using GFramework.Core.architecture;
using GFramework.Core.controller;
using Godot;
using SingleplayerAutobattler.scripts.architecture;

namespace SingleplayerAutobattler.scripts.component;

/// <summary>
/// 玩家区域组件类，继承自 TileMapLayer 并实现 IController 接口。
/// 提供与玩家区域相关的地图操作功能，如坐标转换、边界检测等。
/// </summary>
public partial class PlayerAreaComponent :TileMapLayer,IController
{
	[Export]
	public UnitGridComponent? UnitGrid { get;set; }
	[Export]
	public StoreHighlighterComponent? TileHighlighter { get; set; }
	private Rect2I _bounds;

	/// <summary>
	/// 获取游戏架构实例
	/// </summary>
	/// <returns>返回游戏架构接口实例</returns>
	public IArchitecture GetArchitecture() => GameArchitecture.Instance;

	/// <summary>
	/// 将全局坐标转换为瓦片坐标
	/// </summary>
	/// <param name="global">全局坐标</param>
	/// <returns>对应的瓦片坐标</returns>
	public Vector2I GetTileFromGlobal(Vector2 global)
	{
		return LocalToMap(ToLocal(global));
	}

	/// <summary>
	/// 将瓦片坐标转换为全局坐标
	/// </summary>
	/// <param name="tile">瓦片坐标</param>
	/// <returns>对应的全局坐标</returns>
	public Vector2 GetGlobalFromTile(Vector2I tile)
	{
		return ToGlobal(MapToLocal(tile));
	}

	/// <summary>
	/// 获取鼠标悬停位置对应的瓦片坐标
	/// </summary>
	/// <returns>返回鼠标悬停位置在地图中的瓦片坐标</returns>
	public Vector2I GetHoveredTile()
	{
		return LocalToMap(GetLocalMousePosition());
	}

	/// <summary>
	/// 检查指定的瓦片坐标是否在地图边界范围内
	/// </summary>
	/// <param name="tile">要检查的瓦片坐标</param>
	/// <returns>如果瓦片坐标在边界范围内返回true，否则返回false</returns>
	public bool IsTileInBounds(Vector2I tile)
	{
		return _bounds.HasPoint(tile);
	}

	/// <summary>
	/// 节点准备就绪时的回调方法
	/// 在节点添加到场景树后调用，用于初始化地图边界信息
	/// </summary>
	public override void _Ready()
	{
		// 初始化地图边界矩形区域，起始点为 (0, 0)，大小由 UnitGrid 的 GridSize 决定
		_bounds = new Rect2I(Vector2I.Zero, UnitGrid!.GridSize);
	}
}
