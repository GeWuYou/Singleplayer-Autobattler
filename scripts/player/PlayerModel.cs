using GFramework.Core.model;
using GFramework.SourceGenerators.Abstractions.logging;

namespace SingleplayerAutobattler.scripts.player;

public class PlayerModel: AbstractModel,IPlayerModel
{
    public PlayerDataResource PlayerDataResource { get; set; }
    public int ChangeGold(int value)
    {
        PlayerDataResource.Gold+=value;
        return PlayerDataResource.Gold;
    }
    protected override void OnInit()
    {
        
    }
}