using GFramework.Core.Abstractions.controller;
using GFramework.Core.extensions;
using GFramework.Godot.extensions.signal;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;
using Godot;
using SingleplayerAutobattler.scripts.command;
using SingleplayerAutobattler.scripts.player;

namespace SingleplayerAutobattler.scripts.xp_button;

/// <summary>
/// 经验值购买按钮类
/// 继承自Godot Button控件，实现IController接口
/// 用于处理玩家购买经验值的功能
/// </summary>
[ContextAware]
[Log]
public partial class XpButton : Button, IController
{
    /// <summary>
    /// 获取VBoxContainer子节点
    /// </summary>
    private VBoxContainer VBoxContainer => GetNode<VBoxContainer>("%VBoxContainer");

    /// <summary>
    /// 玩家模型实例
    /// </summary>
    private IPlayerModel _playerModel = null!;

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        _playerModel = this.GetModel<IPlayerModel>()!;
        
        // 订阅玩家数据资源变化信号，当数据变化时刷新按钮状态
        _playerModel
            .PlayerDataResource
            .Signal(Resource.SignalName.Changed)
            .ToAndCall(new Callable(this, nameof(Refresh)))
            .End();
        Pressed+=OnPressed;
    }

    /// <summary>
    /// 按钮被按下时的处理方法
    /// 扣除玩家4个金币并增加4点经验值
    /// </summary>
    private void OnPressed()
    {
        this.SendCommand(new BuyXpCommand(new BuyXpCommandInput
        {
            GoldCost = 4,
            XpAmount = 4
        }));
        var playerModelPlayerDataResource = _playerModel.PlayerDataResource;
        _log.Debug("gold: {0}",playerModelPlayerDataResource.Gold);
        _log.Debug("xp: {0}",playerModelPlayerDataResource.Xp);
        _log.Debug("level: {0}",playerModelPlayerDataResource.Level);
    }

    /// <summary>
    /// 刷新按钮状态的方法
    /// 根据玩家金币数量和等级状态更新按钮的可用性和视觉效果
    /// </summary>
    public void Refresh()
    {
        // 检查玩家是否有足够金币和是否已达到最高等级
        var hasEnoughGold = _playerModel.HasEnoughGold(4);
        var isMaxLevel = _playerModel.IsMaxLevel();
        
        // 根据条件设置按钮是否禁用
        Disabled = !hasEnoughGold || isMaxLevel;
        
        // 根据按钮状态调整VBoxContainer的透明度
        if (hasEnoughGold && !isMaxLevel)
        {
            VBoxContainer.Modulate = VBoxContainer.Modulate with { A = 1 };
        }
        else
        {
            VBoxContainer.Modulate = VBoxContainer.Modulate with { A = 0.5f };
        }
    }
}
