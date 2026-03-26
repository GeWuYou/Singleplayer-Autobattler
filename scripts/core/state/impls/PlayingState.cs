using SingleplayerAutobattler.scripts.enums.scene;

namespace SingleplayerAutobattler.scripts.core.state.impls;

/// <summary>
///     游戏进行中状态
///     表示游戏当前处于运行阶段的状态管理类。
///     继承自ContextAwareStateBase，用于处理游戏运行时的逻辑。
/// </summary>
public class PlayingState : AsyncContextAwareStateBase
{
    public override async Task OnEnterAsync(IState? from)
    {
        var uiRouter = this.GetSystem<IUiRouter>()!;
        await uiRouter.ClearAsync().ConfigureAwait(true);
        await this.GetSystem<ISceneRouter>()!.ReplaceAsync(nameof(SceneKey.Arena)).ConfigureAwait(true);
    }
}
