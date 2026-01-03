using GFramework.Core.Abstractions.controller;
using GFramework.Core.extensions;
using GFramework.Godot.extensions.signal;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;
using Godot;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.enums;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.reroll_button;

[ContextAware]
[Log]
public partial class RerollButton :Button,IController
{
	[Export] public PlayerDataResource PlayerDataResource { get; set; } = null!;
	public HBoxContainer HBoxContainer => GetNode<HBoxContainer>("%HBoxContainer");
	
	private IPlayerModel _playerModel= null!;
	/// <summary>
	/// 节点准备就绪时的回调方法
	/// 在节点添加到场景树后调用
	/// </summary>
	public override void _Ready()
	{
		if (GameConstants.GameMode.IsDev())
		{
			_playerModel = this.GetModel<IPlayerModel>()!;
			_playerModel.PlayerDataResource = PlayerDataResource;
		}

		PlayerDataResource
			.Signal(Resource.SignalName.Changed)
			.ToAndCall(new Callable(this, nameof(Refresh)))
			.End();
		this
			.Signal(BaseButton.SignalName.Pressed)
			.To(new Callable(this, nameof(OnPressed)))
			.End();
	}
	private void Refresh()
	{
		var hasEnoughGold = PlayerDataResource.Gold >= 2;
		Disabled = !hasEnoughGold;
		if (hasEnoughGold)
		{
			HBoxContainer.Modulate = HBoxContainer.Modulate with { A = 1.0f };
		}else
		{
			HBoxContainer.Modulate = HBoxContainer.Modulate with { A = 0.5f };
		}
	}
	private void OnPressed()
	{
		_playerModel.ChangeGold(-2);
	}
}