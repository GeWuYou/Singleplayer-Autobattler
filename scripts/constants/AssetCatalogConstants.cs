using GFramework.Core.Godot.system;

namespace SingleplayerAutobattler.scripts.constants;

/// <summary>
/// 资源目录常量类，用于定义游戏中各种资产的映射关系
/// </summary>
public static class AssetCatalogConstants
{
    /// <summary>
    /// 场景资源目录类，包含所有场景类型的资产映射
    /// </summary>
    public static class AssetCatalogScene
    {
        /// <summary>
        /// 单位场景资产映射，将单位标识符映射到具体的场景文件路径
        /// </summary>
        public static readonly AssetCatalog.AssetCatalogMapping Unit = new("Unit",new AssetCatalog.SceneId("res://scenes/unit/unit.tscn"));
    }
    
    /// <summary>
    /// 资源目录类，包含所有资源类型的资产映射
    /// </summary>
    public static class AssetCatalogResource
    {
        /// <summary>
        /// Robin角色资源资产映射，将Robin标识符映射到具体的角色资源配置文件路径
        /// </summary>
        public static readonly AssetCatalog.AssetCatalogMapping Robin = new("Robin",new AssetCatalog.ResourceId("res://resource/unit/robin.tres"));
    }
}
