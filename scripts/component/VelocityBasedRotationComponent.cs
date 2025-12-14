using System;
using System.Threading.Tasks;
using GFramework.Core.architecture;
using GFramework.Core.controller;
using GFramework.Core.events;
using GFramework.Core.extensions;
using GFramework.Core.Godot.extensions;
using Godot;
using SingleplayerAutobattler.scripts.architecture;

namespace SingleplayerAutobattler.scripts.component;

/// <summary>
/// 基于速度的旋转组件，根据目标节点的水平移动速度动态调整其旋转角度。
/// 当目标节点的X轴速度超过阈值时，会根据速度方向和大小计算一个旋转角度，并通过插值平滑应用到目标节点上。
/// </summary>
public partial class VelocityBasedRotationComponent : Node, IController
{
    /// <summary>
    /// 是否启用该旋转控制逻辑。设置此属性将自动重置目标节点的旋转（如果禁用）。
    /// </summary>
    [Export]
    public bool Enable
    {
        get => _enable;
        set
        {
            _enable = value;
            _ = SetEnable();
        }
    }

    /// <summary>
    /// 异步设置启用状态，在节点准备就绪后执行相关初始化操作。
    /// 如果当前处于禁用状态且目标节点有效，则将其旋转归零。
    /// </summary>
    private async Task SetEnable()
    {
        await this.WaitUntilReady();
        if (Target.IsValidNode() && !Enable)
        {
            Target.Rotation = 0;
        }
    }

    /// <summary>
    /// 目标节点，该组件将基于此节点的速度来控制其旋转行为。
    /// </summary>
    [Export]
    public Node2D Target { get; set; }

    /// <summary>
    /// 插值时间（秒），用于控制旋转变化的平滑程度。
    /// 取值范围应在0.25至1.5之间，默认为0.4秒。
    /// </summary>
    [Export(PropertyHint.Range, "0.25,1.5")]
    public float LerpSeconds { get; set; } = 0.4f;

    /// <summary>
    /// 旋转倍数因子，用于放大或缩小由速度决定的基础旋转角度。
    /// 默认值为120。
    /// </summary>
    [Export]
    public int MaxRotationDegrees { get; set; } = 50;

    /// <summary>
    /// X轴速度阈值，只有当目标节点的X轴绝对速度大于该值时才会触发旋转效果。
    /// 默认值为3.0。
    /// </summary>
    [Export]
    public float XVelocityThreshold { get; set; } = 3.0f;

    /// <summary>
    /// 表示一个具有位置、速度、角度和进度等物理属性的对象
    /// </summary>
    protected Vector2 LastPosition;

    /// <summary>
    /// 表示对象当前的速度向量，包含X和Y方向的速度分量
    /// </summary>
    protected Vector2 Velocity;

    /// <summary>
    /// 表示对象的旋转角度，以弧度为单位
    /// </summary>
    protected double Angle;

    /// <summary>
    /// 表示对象的执行进度，通常用于动画或任务完成度的跟踪
    /// </summary>
    protected float Progress;

    /// <summary>
    /// 表示自某个起始时间点以来经过的时间，以秒为单位
    /// </summary>
    protected float TimeElapsed;



    /// <summary>
    /// 物理过程处理函数，在每个物理帧中执行逻辑更新
    /// </summary>
    /// <param name="delta">距离上一帧的时长（秒）</param>
    public override void _PhysicsProcess(double delta) {
        // 若未启用或目标无效则跳过处理
        if (!Enable || Target.IsInvalidNode())
        {
            return;
        }

        // 计算当前帧与上一帧之间的位移向量作为速度估计
        Velocity = Target.GlobalPosition - LastPosition;
        LastPosition = Target.GlobalPosition;

        // 更新插值进度
        Progress = TimeElapsed / LerpSeconds;

        // 判断是否满足旋转条件并计算目标角度
        if (Math.Abs(Velocity.X) > XVelocityThreshold)
        {
            Angle = Velocity.Normalized().X * Mathf.DegToRad(MaxRotationDegrees);
        }
        else
        {
            Angle = 0;
        }

        // 使用角度线性插值更新目标节点的旋转
        Target.Rotation = (float)Mathf.LerpAngle(Target.Rotation, Angle, Progress);

        // 更新计时器，若完成一次完整插值周期则重置时间
        TimeElapsed += (float)delta;
        if (Progress > 1.0f)
        {
            TimeElapsed = 0;
        }
    }


    /// <summary>
    /// 取消注册列表，用于管理需要在节点销毁时取消注册的对象
    /// </summary>
    private readonly IUnRegisterList _unRegisterList = new UnRegisterList();

    private bool _enable = true;

    /// <summary>
    /// 获取游戏架构实例
    /// </summary>
    /// <returns>返回游戏架构接口实例</returns>
    public IArchitecture GetArchitecture() => GameArchitecture.Instance;

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
    }

    /// <summary>
    /// 节点退出场景树时的回调方法
    /// 在节点从场景树移除前调用，用于清理资源
    /// </summary>
    public override void _ExitTree() => _unRegisterList.UnRegisterAll();
}