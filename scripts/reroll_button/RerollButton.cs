using GFramework.Core.Abstractions.controller;
using GFramework.Godot.extensions.signal;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;
using Godot;
using SingleplayerAutobattler.scripts.command;
using SingleplayerAutobattler.scripts.player;
using SingleplayerAutobattler.scripts.system;

namespace SingleplayerAutobattler.scripts.reroll_button;

[ContextAware]
[Log]
public partial class RerollButton : Button, IController
{
    private IPlayerModel _playerModel = null!;
    private IRerollSystem _rerollSystem = null!;
    public HBoxContainer HBoxContainer => GetNode<HBoxContainer>("%HBoxContainer");

    /// <summary>
    ///     节点准备就绪时的回调方法
    ///     在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        _playerModel = this.GetModel<IPlayerModel>()!;
        _rerollSystem = this.GetSystem<IRerollSystem>()!;
        _playerModel.PlayerDataResource
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
        Disabled = !_rerollSystem.CanReroll(_playerModel.PlayerDataResource);
        HBoxContainer.Modulate = Disabled
            ? HBoxContainer.Modulate with { A = 0.5f }
            : HBoxContainer.Modulate with { A = 1.0f };
    }

    private void OnPressed()
    {
        this.SendCommand(new RerollCommand(new RerollCommandInput()));
    }
}