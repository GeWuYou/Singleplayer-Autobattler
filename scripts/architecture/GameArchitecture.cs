using GFramework.Core.Godot.architecture;
using GFramework.Core.Godot.system;
using SingleplayerAutobattler.scripts.arena;
using SingleplayerAutobattler.scripts.system;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.architecture;

/// <summary>
/// GameArchitecture
/// </summary>
public class GameArchitecture: AbstractArchitecture<GameArchitecture>
{
    protected override void RegisterUtilities()
    {
       RegisterUtility(new UnitMapper());
    }

    protected override void RegisterSystems()
    {
       RegisterSystem(new DataParseSystem());
       RegisterSystem<IAssetCatalogSystem>(new AssetCatalogSystem());
       RegisterSystem<IResourceLoadSystem>(new ResourceLoadSystem());
       RegisterSystem<IResourceFactorySystem>(new ResourceFactorySystem());
    }

    protected override void RegisterModels()
    {
       RegisterModel(new ArenaModel());
       RegisterModel(new UnitModel());
    }
}