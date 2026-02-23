using GFramework.Core.Abstractions.environment;
using GFramework.Core.model;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.environment;

namespace SingleplayerAutobattler.scripts.player;

public class PlayerModel : AbstractModel, IPlayerModel
{
    public PlayerDataResource PlayerDataResource { get; set; } = null!;

    public int ChangeGold(int value)
    {
        PlayerDataResource.Gold += value;
        return PlayerDataResource.Gold;
    }

    public void GainXp(int amount)
    {
        PlayerDataResource.Xp += amount;
    }

    public bool HasEnoughGold(int cost)
    {
        return PlayerDataResource.Gold >= cost;
    }

    public bool IsMaxLevel()
    {
        return PlayerDataResource.Level >= PlayerDataResource.MaxLevel;
    }

    protected override void OnInit()
    {
        var env = this.GetEnvironment<IEnvironment>()!;
        if (GameConstants.Development.Equals(env.Name))
            env.IfType<GameDevEnvironment>(e => { PlayerDataResource = e.PlayerDataResource; }
            );
    }
}