using Godot;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.gold_display_view;

/// <summary>
///     金币显示视图类
///     继承自HBoxContainer并实现IController接口，用于显示玩家的金币数量
/// </summary>
[ContextAware]
[Log]
public partial class GoldDisplayView : HBoxContainer, IController
{
	/// <summary>
	///     玩家数据资源，用于获取玩家的金币信息
	/// </summary>
	[Export]
    public PlayerDataResource PlayerDataResource { get; set; } = null!;

	/// <summary>
	///     显示金币数量的文本标签
	/// </summary>
	[Export]
    public Label GoldText { get; set; } = null!;

	/// <summary>
	///     节点准备就绪时的回调方法
	///     在节点添加到场景树后调用
	/// </summary>
	public override void _Ready()
    {
        // 订阅玩家数据变化事件
        PlayerDataResource.Changed += OnPlayerDataChanged;
        // 初始化显示当前金币数量
        OnPlayerDataChanged();
    }

	/// <summary>
	///     玩家数据变化时的回调方法
	///     更新金币显示文本
	/// </summary>
	private void OnPlayerDataChanged()
    {
        // 将玩家数据资源中的金币数量转换为字符串并更新显示
        GoldText.Text = PlayerDataResource.Gold.ToString();
    }
}