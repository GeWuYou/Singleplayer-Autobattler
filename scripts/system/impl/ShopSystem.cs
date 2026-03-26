using SingleplayerAutobattler.scripts.player;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.system.impl;

public class ShopSystem : AbstractSystem, IShopSystem
{
    public bool CanBuyUnit(PlayerDataResource playerData, UnitDataResource unitData)
    {
        return playerData.Gold >= unitData.GoldCost;
    }

    protected override void OnInit()
    {
    }
}