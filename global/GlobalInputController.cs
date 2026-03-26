using SingleplayerAutobattler.scripts.core.controller;
using SingleplayerAutobattler.scripts.core.state.impls;
using SingleplayerAutobattler.scripts.cqrs.game.command;
using SingleplayerAutobattler.scripts.cqrs.pause_menu.command.input;
using SingleplayerAutobattler.scripts.enums;
using SingleplayerAutobattler.scripts.constants;
using Godot;

namespace SingleplayerAutobattler.global;

/// <summary>
///     全局输入控制器类，继承自 GameInputController。
///     负责处理游戏中的全局输入事件，包括暂停和恢复游戏的功能。
/// </summary>
[ContextAware]
[Log]
public partial class GlobalInputController : GameInputController
{
    private UiHandle? _pauseMenuUiHandle;

    /// <summary>
    ///     状态机系统实例，用于管理游戏状态。
    /// </summary>
    private IStateMachineSystem _stateMachineSystem = null!;

    /// <summary>
    ///     初始化方法，在节点准备就绪时调用。
    ///     获取并初始化状态机系统实例。
    /// </summary>
    public override void _Ready()
    {
        EnsureInputActions();
        _stateMachineSystem = this.GetSystem<IStateMachineSystem>()!;
    }

    private static void EnsureInputActions()
    {
        EnsureMouseAction(InputActionConstants.Select, MouseButton.Left);
        EnsureMouseAction(InputActionConstants.CancelDrag, MouseButton.Right);
        EnsureKeyAction(InputActionConstants.QuickSell, Key.Q);
    }

    private static void EnsureMouseAction(string actionName, MouseButton button)
    {
        if (InputMap.HasAction(actionName)) return;

        InputMap.AddAction(actionName);
        InputMap.ActionAddEvent(actionName, new InputEventMouseButton { ButtonIndex = button });
    }

    private static void EnsureKeyAction(string actionName, Key key)
    {
        if (InputMap.HasAction(actionName)) return;

        InputMap.AddAction(actionName);
        InputMap.ActionAddEvent(actionName, new InputEventKey { Keycode = key });
    }

    protected override bool AcceptPhase(InputPhase phase)
    {
        return phase is InputPhase.Global or InputPhase.Paused;
    }

    protected override void Handle(InputPhase phase, InputEvent @event)
    {
        // 检查是否按下了取消操作（通常是 ESC 键）
        if (!@event.IsActionPressed("ui_cancel"))
            return;

        // 根据当前状态执行相应操作
        if (_stateMachineSystem.Current is not PlayingState) return;
        _log.Debug("暂停游戏");
        _pauseMenuUiHandle = this.SendCommand(new PauseGameWithOpenPauseMenuCommand(new OpenPauseMenuCommandInput
            { Handle = _pauseMenuUiHandle }));
        GetViewport().SetInputAsHandled();
    }
}
