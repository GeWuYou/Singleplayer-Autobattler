using Godot;
using SingleplayerAutobattler.scripts.command;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.xp_tracker;

[ContextAware]
[Log]
public partial class XpTracker : VBoxContainer, IController
{
    private IPlayerModel _playerModel = null!;

    private ProgressBar ProgressBar => GetNode<ProgressBar>("%ProgressBar");
    private Label XpLabel => GetNode<Label>("%XpLabel");
    private Label LevelLabel => GetNode<Label>("%LevelLabel");

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept"))
            this.SendCommand(new BuyXpCommand(new BuyXpCommandInput
            {
                XpAmount = 4,
                GoldCost = 4
            }));
    }

    /// <summary>
    ///     节点准备就绪时的回调方法
    ///     在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        _playerModel = this.GetModel<IPlayerModel>()!;
        _playerModel
            .PlayerDataResource
            .Signal(Resource.SignalName.Changed)
            .ToAndCall(new Callable(this, nameof(Refresh)))
            .End();
    }

    public void Refresh()
    {
        var playerDataResource = _playerModel.PlayerDataResource;
        if (!_playerModel.IsMaxLevel())
            SetXpBarValue();
        else
            SetMaxLevelValue();

        LevelLabel.Text = $"lvl: {playerDataResource.Level}";
    }

    private void SetXpBarValue()
    {
        var player = _playerModel.PlayerDataResource;

        var requiredXp = player.GetCurrentXpRequirement();

        ProgressBar.MinValue = 0;
        ProgressBar.MaxValue = requiredXp;
        ProgressBar.Value = player.Xp;

        XpLabel.Text = $"{player.Xp}/{requiredXp}";
    }

    private void SetMaxLevelValue()
    {
        XpLabel.Text = "max";
        ProgressBar.Value = 100;
    }
}