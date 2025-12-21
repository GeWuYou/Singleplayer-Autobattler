using SingleplayerAutobattler.scripts.data;
using SingleplayerAutobattler.scripts.interfaces;

namespace SingleplayerAutobattler.scripts.player;

/// <summary>
/// 玩家数据类，用于存储和管理玩家的相关数据信息。
/// 继承自BaseData基类，扩展玩家特定的数据属性和功能。
/// </summary>
public class PlayerData : BaseData,ILevel
{
	/// <summary>
	/// 玩家拥有的金币数量
	/// </summary>
	public int Gold { get; set; }

	/// <summary>
	/// 玩家当前经验值
	/// </summary>
	public int Xp { get; set; }

	/// <summary>
	/// 玩家等级
	/// </summary>
	public int Level { get; set; } = 1;
}
