using GFramework.Game.assets;

namespace SingleplayerAutobattler.scripts.constants;

/// <summary>
/// 资源目录常量类，用于定义游戏中各种资产的映射关系
/// </summary>
public static class AssetCatalogConstants
{
    /// <summary>
    /// 场景单元资源目录映射类，包含游戏中场景单元的资产映射定义
    /// </summary>
    public static class AssetCatalogSceneUnit
    {
        /// <summary>
        /// 单元场景资产映射，将"Unit"标识符映射到单元场景文件路径
        /// </summary>
        public static readonly AssetCatalog.AssetCatalogMapping Unit = new("Unit",new AssetCatalog.SceneUnitId("res://scenes/unit/unit.tscn"));
    }
    

    /// <summary>
    /// 资源资产目录映射类，包含游戏中各种资源资产的映射定义
    /// </summary>
    public static class AssetCatalogResource
    {
        /// <summary>
        /// Robin角色资源资产映射，将Robin标识符映射到具体的角色资源配置文件路径
        /// </summary>
        public static readonly AssetCatalog.AssetCatalogMapping Robin = new("Robin",new AssetCatalog.AssetId("res://resource/unit/robin.tres"));
    }
}

