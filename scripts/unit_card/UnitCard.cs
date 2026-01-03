using System.Threading.Tasks;
using GFramework.Core.Abstractions.controller;
using GFramework.Core.extensions;
using GFramework.Godot.extensions;
using GFramework.Godot.extensions.signal;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;
using Godot;
using SingleplayerAutobattler.scripts.command;
using SingleplayerAutobattler.scripts.constants;
using SingleplayerAutobattler.scripts.enums;
using SingleplayerAutobattler.scripts.player;
using SingleplayerAutobattler.scripts.shop;
using SingleplayerAutobattler.scripts.system;
using SingleplayerAutobattler.scripts.unit;

namespace SingleplayerAutobattler.scripts.unit_card;

[ContextAware]
[Log]
public partial class UnitCard : Button, IController
{
    [Signal]
    public delegate void UnitBoughtEventHandler(UnitDataResource unitData);

    private static readonly Color HoverBorderColor = new("fafa82");
    private UnitDataResource? _unitDataResource;

    private StyleBoxFlat _borderSb = null!;
    private StyleBoxFlat _bottomSb = null!;
    private Color _borderColor;
    [Export] public PlayerDataResource PlayerDataResource { get; set; } = null!;


    [Export]
    public UnitDataResource? UnitDataResource
    {
        get => _unitDataResource;
        set
        {
            _unitDataResource = value;
            _ = SetUnitDataResource();
        }
    }

    public Label Traits => GetNode<Label>("%Traits");
    public Panel Bottom => GetNode<Panel>("%Bottom");
    public Label UnitName => GetNode<Label>("%UnitName");
    public Label GoldCost => GetNode<Label>("%GoldCost");
    public Panel Border => GetNode<Panel>("%Border");
    public Panel EmptyPlaceholder => GetNode<Panel>("%EmptyPlaceholder");
    public TextureRect UnitIcon => GetNode<TextureRect>("%UnitIcon");
    private IShopSystem _shopSystem = null!;
    private IShopModel _shopModel = null!;
    private IPlayerModel _playerModel= null!;

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        if (GameConstants.GameMode.IsDev())
        {
            _playerModel = this.GetModel<IPlayerModel>()!;
            _playerModel.PlayerDataResource = PlayerDataResource;
        }

        // 获取系统
        _shopSystem = this.GetSystem<IShopSystem>()!;

        // 获取模型
        _shopModel = this.GetModel<IShopModel>()!;

        const string name = "panel";
        _borderSb = (Border.GetThemeStylebox(name) as StyleBoxFlat)!;
        _bottomSb = (Bottom.GetThemeStylebox(name) as StyleBoxFlat)!;

        PlayerDataResource
            .Signal(Resource.SignalName.Changed)
            .ToAndCall(new Callable(this, nameof(RefreshView)))
            .End();
        this
            .Signal(SignalName.UnitBought)
            .To(Callable.From<UnitDataResource>(_ => { _log.Debug("gold: {0}", PlayerDataResource.Gold); }))
            .End()
            .Signal(BaseButton.SignalName.Pressed)
            .To(new Callable(this, nameof(OnBuyPressed)))
            .End()
            .Signal(Control.SignalName.MouseEntered)
            .To(new Callable(this, nameof(OnMouseEntered)))
            .End()
            .Signal(Control.SignalName.MouseExited)
            .To(new Callable(this, nameof(OnMouseExited)))
            .End();
    }

    /// <summary>
    /// 异步设置单位数据资源，初始化单位显示界面元素
    /// </summary>
    /// <returns>异步任务</returns>
    private async Task SetUnitDataResource()
    {
        await this.WaitUntilReady();

        // 检查单位数据资源是否为空，如果为空则显示占位符并禁用功能
        if (UnitDataResource is null)
        {
            EmptyPlaceholder.Show();
            Disabled = true;
            return;
        }

        // 设置边框颜色和底部背景色
        _borderColor = UnitDataResource.Rarity.GetColor();
        _borderSb.BorderColor = _borderColor;
        _bottomSb.BgColor = _borderColor;

        // 设置单位名称和金币成本显示
        UnitName.Text = UnitDataResource.Name;
        GoldCost.Text = $"{UnitDataResource.GoldCost}G";

        // 更新单位图标的纹理坐标
        UnitIcon.Texture.IfType<AtlasTexture>(texture =>
        {
            var region = texture.Region;
            region.Position = UnitDataResource.SkinCoordinates * ArenaConstants.CellSizeVector;
            texture.Region = region;
        });
    }

    private void OnMouseExited()
    {
        _borderSb.BorderColor = _borderColor;
    }

    private void OnMouseEntered()
    {
        if (!Disabled)
        {
            _borderSb.BorderColor = HoverBorderColor;
        }
    }

    private void OnBuyPressed()
    {
        if (UnitDataResource is null)
        {
            return;
        }

        if (!this.SendCommand(new BuyUnitCommand(UnitDataResource)))
        {
            return;
        }

        EmitSignalUnitBought(UnitDataResource);
    }

    private void RefreshView()
    {
        if (UnitDataResource is null)
        {
            return;
        }

        var isBought = _shopModel.IsBought(UnitDataResource);
        var canBuy = _shopSystem.CanBuyUnit(playerData: PlayerDataResource, unitData: UnitDataResource);
        Disabled = !canBuy;
        _log.Debug("isBought: {0}, canBuy: {1}", isBought, canBuy);
        EmptyPlaceholder.Visible = isBought;
        if (canBuy || isBought)
        {
            Modulate = new Color(Colors.White);
        }
        else
        {
            Modulate = new Color(Colors.White, .5f);
        }
    }
}