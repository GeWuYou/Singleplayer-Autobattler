using GFramework.Core.extensions;
using GFramework.Core.model;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.environment;

namespace SingleplayerAutobattler.scripts.player;

public class PlayerModel: AbstractModel,IPlayerModel
{
    public PlayerDataResource PlayerDataResource { get; set; } = null!;
    protected override void OnInit()
    {
        var env = this.GetEnvironment<GameDevEnvironment>()!;
        if (GameConstants.Development.Equals(env.Name))
        {
            PlayerDataResource = env.PlayerDataResource;
        }
    }
    public int ChangeGold(int value)
    {
        PlayerDataResource.Gold+=value;
        return PlayerDataResource.Gold;
    }
    
}