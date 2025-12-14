using System.Collections.Generic;
using GFramework.Core.model;
using Godot;

namespace SingleplayerAutobattler.scripts.unit;

public class UnitModel : AbstractModel, IUnitModel
{
    public Dictionary<int, UnitData> UnitDataDictionary { get; set; } = [];

    public Dictionary<Vector2I, UnitData?> UnitDictionary { get; set; } = [];

    protected override void OnInit()
    {
    }
}