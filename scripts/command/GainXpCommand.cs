
using GFramework.Core.command;
using GFramework.Core.extensions;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.command;

public class GainXpCommand(int amount) : AbstractCommand
{
    protected override void OnExecute()
    {
        var player = this.GetModel<IPlayerModel>();
        player.GainXp(amount);
    }
}
