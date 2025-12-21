using Godot;

namespace SingleplayerAutobattler.scripts.player;

/// <summary>
/// 玩家数据资源类，用于存储和管理玩家的游戏数据
/// 继承自Godot的Resource类，可作为游戏资源配置使用
/// </summary>
[GlobalClass]
public partial class PlayerDataResource:  Resource
{
    private int _gold;
    private int _xp;
    private int _level;

    /// <summary>
    /// 玩家拥有的金币数量
    /// 取值范围：0-99
    /// </summary>
    [Export(PropertyHint.Range, "0,99")]
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            // 当金币数值发生变化时，触发资源变更事件
            EmitChanged();
        }
    }

    /// <summary>
    /// 玩家当前经验值
    /// 取值范围：0-99
    /// </summary>
    [Export(PropertyHint.Range, "0,99")]
    public int Xp
    {
        get => _xp;
        set
        {
            _xp = value;
            // 当经验值发生变化时，触发资源变更事件
            EmitChanged();
        }
    }

    /// <summary>
    /// 玩家等级
    /// 取值范围：1-6
    /// </summary>
    [Export(PropertyHint.Range, "1,6")]
    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            // 当等级发生变化时，触发资源变更事件
            EmitChanged();
        }
    }
}

