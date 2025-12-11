using System.Collections.Generic;
using GFramework.Core.model;

namespace SingleplayerAutobattler.scripts.unit;

public class UnitModel : AbstractModel,IUnitModel
{
    public Dictionary<int, UnitData> UnitDictionary { get; set; }

    protected override void OnInit()
    {

    }
}