using GFramework.Game.input;

namespace SingleplayerAutobattler.scripts.input;

/// <summary>
/// 游戏输入事件静态类，用于定义和管理游戏中的输入事件
/// </summary>
public static class GameInputEvents
{
    /// <summary>
    /// 取消拖拽操作的输入事件
    /// 该事件用于处理用户取消当前拖拽操作的输入响应
    /// </summary>
    public static readonly InputEvents.KeyInputEvent CancelDrag = new("cancel_drag", true,false);
}
