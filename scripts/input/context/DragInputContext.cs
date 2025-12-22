using System;
using GFramework.Game.input;
using Godot;

namespace SingleplayerAutobattler.scripts.input.context;

/// <summary>
/// 拖拽输入上下文类，用于处理拖拽操作的取消输入
/// </summary>
/// <param name="cancel">取消拖拽操作的回调函数</param>
public sealed class DragInputContext(Action cancel) : IInputContext
{
    /// <summary>
    /// 处理游戏输入事件，检测是否为拖拽取消操作
    /// </summary>
    /// <param name="input">要处理的游戏输入事件</param>
    /// <returns>如果处理了输入事件则返回true，否则返回false</returns>
    public bool Handle(IGameInputEvent input)
    {
        if (input is InputEvents.KeyInputEvent key1)
        {
            GD.Print($"{key1.Action},{key1.Pressed}");
        }
        // 检查输入是否为按键事件，并且是取消拖拽的按键操作
        if (input is not InputEvents.KeyInputEvent key ||
            key.Action != GameInputEvents.CancelDrag.Action ||
            !key.Pressed)
        {
            return false;
        }
        
        // 执行取消拖拽的回调函数
        cancel();
        
        // 吃掉输入
        return true;
    }
}

