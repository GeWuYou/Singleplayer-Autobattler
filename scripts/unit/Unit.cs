using System.Threading.Tasks;
using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Core.events;
using GFramework.Core.extensions;
using GFramework.Core.Godot.extensions;
using Godot;
using SingleplayerAutobattler.script.architecture;
using SingleplayerAutobattler.scripts.constants;

namespace SingleplayerAutobattler.scripts.unit;

public partial class Unit : Area2D, IController
{
    [Export]
    public Sprite2D Skin { get; set; }
    [Export]
    public ProgressBar HealthBar { get; set; }
    [Export]
    public ProgressBar ManaBar { get; set; }

    [Export]
    public UnitDataResource UnitDataResource
    {
        get => _unitDataResource;
        set
        {
            _unitDataResource = value;
            _ = SetUnitDataResource(UnitDataResource);
        }
    }

    private UnitModel _unitModel;
    private UnitMapper _unitMapper;
    /// <summary>
    /// 取消注册列表，用于管理需要在节点销毁时取消注册的对象
    /// </summary>
    private IUnRegisterList _unRegisterList = new UnRegisterList();

    private UnitDataResource _unitDataResource;

    /// <summary>
    /// 获取游戏架构实例
    /// </summary>
    /// <returns>返回游戏架构接口实例</returns>
    public IArchitecture GetArchitecture() => GameArchitecture.Interface;

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        _unitModel = this.GetModel<UnitModel>();
        _unitMapper = this.GetUtility<UnitMapper>();
    }
    
    /// <summary>
    /// 设置单位数据资源并更新皮肤区域位置
    /// </summary>
    /// <param name="unitDataResource">单位数据资源对象，包含皮肤坐标等信息</param>
    /// <returns>异步任务</returns>
    public async Task SetUnitDataResource(UnitDataResource unitDataResource)
    {
        // 如果传入的单位数据资源为空，则直接返回
        if (unitDataResource is null)
        {
            return;
        }
        
        // 等待资源准备就绪
        await this.WaitUntilReady();
        
        // 根据单位数据资源中的皮肤坐标更新皮肤区域的位置
        Skin.RegionRect = Skin.RegionRect with
        {
            Position = unitDataResource.SkinCoordinates * ArenaConstants.CellSizeVector
        };
        // 将单位数据资源转换为游戏单位数据
        _unitModel.UnitDictionary[UnitDataResource.Id] = _unitMapper.DataFromResource(unitDataResource);
    }


    /// <summary>
    /// 节点退出场景树时的回调方法
    /// 在节点从场景树移除前调用，用于清理资源
    /// </summary>
    public override void _ExitTree() => _unRegisterList.UnRegisterAll();
}