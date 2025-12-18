using GFramework.Core.Godot.system;
using SingleplayerAutobattler.scripts.constants;

namespace SingleplayerAutobattler.scripts.system;

/// <summary>
/// 资源目录系统类，用于管理和注册游戏中的各种资源
/// </summary>
public class AssetCatalogSystem : AbstractAssetCatalogSystem
{
    /// <summary>
    /// 注册游戏所需的所有资源
    /// 该方法在系统初始化时被调用，用于将资源键与资源路径进行映射关联
    /// </summary>
    protected override void RegisterAssets()
    {
        // 注册单位场景资源，将单位场景键与对应的资源路径进行绑定
        RegisterScene(AssetCatalogConstants.AssetCatalogScene.Unit);
        RegisterResource(AssetCatalogConstants.AssetCatalogResource.Robin);
    }
}