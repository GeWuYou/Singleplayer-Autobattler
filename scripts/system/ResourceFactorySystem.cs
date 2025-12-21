
using GFramework.Godot.system;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.system;

/// <summary>
/// 资源工厂系统，负责管理和创建各种游戏资源的工厂实例。
/// 该系统通过注册表管理不同类型的资源工厂，并支持场景和资源的预加载功能。
/// </summary>
public class ResourceFactorySystem : AbstractResourceFactorySystem
{
    public struct ArchitectureInitializedEvent
    {
        
    }
    /// <summary>
    /// 注册系统所需的各种资源类型。
    /// </summary>
    protected override void RegisterResources()
    {
        RegisterSceneUnit<Unit>(AssetCatalogConstants.AssetCatalogSceneUnit.Unit.Key, true);
        RegisterAsset<UnitDataResource>(AssetCatalogConstants.AssetCatalogResource.Robin.Key,true);
    }
}