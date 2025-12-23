using System.Threading.Tasks;
using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Core.extensions;
using GFramework.Godot.extensions;
using Godot;
using SingleplayerAutobattler.scripts.architecture;
using SingleplayerAutobattler.scripts.component;
using SingleplayerAutobattler.scripts.constants;

namespace SingleplayerAutobattler.scripts.unit;

/// <summary>
/// 表示一个游戏中的单位实体，继承自Area2D并实现IController接口。
/// 提供单位的基本行为、交互逻辑以及与系统其他部分的通信能力。
/// </summary>
public partial class Unit : Area2D, IController
{
    [Signal]
    public delegate void QuickSellPressedEventHandler();
    [Export] public Sprite2D? Skin { get; set; }
    [Export] public ProgressBar? HealthBar { get; set; }
    [Export] public ProgressBar? ManaBar { get; set; }

    [Export]
    public UnitDataResource? UnitDataResource
    {
        get => _unitDataResource;
        set
        {
            _unitDataResource = value;
            _ = SetUnitDataResource(UnitDataResource);
        }
    }

    [Export] public DragDropComponent? DragDropComponent { get; set; }
    [Export] public OutlineHighlighter? OutlineHighlighter { get; set; }
    [Export] public VelocityBasedRotationComponent? VelocityBasedRotationComponent { get; set; }

    private UnitModel? _unitModel;
    private UnitMapper? _unitMapper;

    private UnitDataResource? _unitDataResource;
    private bool _isHovered;

    /// <summary>
    /// 获取游戏架构实例
    /// </summary>
    /// <returns>返回游戏架构接口实例</returns>
    public IArchitecture GetArchitecture() => GameArchitecture.Instance;

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用，初始化模型引用、事件监听及信号连接
    /// </summary>
    public override void _Ready()
    {
        _unitModel = this.GetModel<UnitModel>();
        _unitMapper = this.GetUtility<UnitMapper>();
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        DragDropComponent!.Connect(DragDropComponent.SignalName.DragStarted, new Callable(this, nameof(OnDragStarted)));
        DragDropComponent!.Connect(DragDropComponent.SignalName.DragCanceled, new Callable(this, nameof(OnDragCanceled)));
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(InputActionConstants.QuickSell)&& _isHovered)
        {
            EmitSignalQuickSellPressed();
        }
    }

    /// <summary>
    /// 当拖拽开始时触发的方法，启用基于速度旋转组件
    /// </summary>
    private void OnDragStarted()
    {
        VelocityBasedRotationComponent!.Enable = true;
    }

    /// <summary>
    /// 拖拽被取消时触发的方法，恢复单位至起始位置并关闭旋转效果
    /// </summary>
    /// <param name="startingPosition">拖拽开始时的位置</param>
    private void OnDragCanceled(Vector2 startingPosition)
    {
        RestAfterDragging(startingPosition);
    }

    /// <summary>
    /// 拖拽结束后重置单位状态：禁用旋转组件并将单位放回指定位置
    /// </summary>
    /// <param name="startingPosition">要放置的目标位置</param>
    public void RestAfterDragging(Vector2 startingPosition)
    {
        VelocityBasedRotationComponent!.Enable = false;
        GlobalPosition = startingPosition;
    }

    /// <summary>
    /// 鼠标离开该单位区域时触发的方法
    /// 若当前正在拖拽则忽略操作；否则清除高亮显示，并将层级设为默认值
    /// </summary>
    private void OnMouseExited()
    {
        if (DragDropComponent!.IsDragging)
        {
            return;
        }
        _isHovered = false;
        OutlineHighlighter!.ClearHighlight();
        ZIndex = ZIndexConstants.Zero;
    }

    /// <summary>
    /// 鼠标进入该单位区域时触发的方法
    /// 若当前正在拖拽则忽略操作；否则进行高亮显示，并提升渲染层级以确保可见性
    /// </summary>
    private void OnMouseEntered()
    {
        if (DragDropComponent!.IsDragging)
        {
            return;
        }
        _isHovered = true;
        OutlineHighlighter!.Highlight();
        ZIndex = ZIndexConstants.One;
    }

    /// <summary>
    /// 设置单位数据资源并更新皮肤区域位置
    /// </summary>
    /// <param name="unitDataResource">单位数据资源对象，包含皮肤坐标等信息</param>
    /// <returns>异步任务</returns>
    public async Task SetUnitDataResource(UnitDataResource? unitDataResource)
    {
        // 如果传入的单位数据资源为空，则直接返回
        if (unitDataResource is null)
        {
            return;
        }

        // 等待资源准备就绪
        await this.WaitUntilReady();

        // 根据单位数据资源中的皮肤坐标更新皮肤区域的位置
        Skin!.RegionRect = Skin.RegionRect with
        {
            Position = unitDataResource.SkinCoordinates * ArenaConstants.CellSizeVector
        };
        // 将单位数据资源转换为游戏单位数据
        _unitModel!.UnitDataDictionary[UnitDataResource!.Id] = _unitMapper!.DataFromResource(unitDataResource);
    }
}
