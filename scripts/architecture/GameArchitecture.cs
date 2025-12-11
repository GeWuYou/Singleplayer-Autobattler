
using GFramework.Core.architecture;
using SingleplayerAutobattler.scripts.arena;
using SingleplayerAutobattler.scripts.system;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.script.architecture;

/// <summary>
/// GameArchitecture
/// </summary>
public class GameArchitecture: Architecture<GameArchitecture>
{
    protected override void Init()
    {
        RegisterModels();
        RegisterSystems();
        RegisterUtilitys();
    }

    private void RegisterUtilitys()
    {
       RegisterUtility(new UnitMapper());
    }

    private void RegisterSystems()
    {
       RegisterSystem(new DataParseSystem());
    }

    private void RegisterModels()
    {
       RegisterModel(new ArenaModel());
       RegisterModel(new UnitModel());
    }
}