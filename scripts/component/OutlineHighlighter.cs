using GFramework.Core.Abstractions.architecture;
using GFramework.Core.Abstractions.controller;
using GFramework.SourceGenerators.Abstractions.rule;
using Godot;
using SingleplayerAutobattler.scripts.architecture;

namespace SingleplayerAutobattler.scripts.component;

[ContextAware]
public partial class OutlineHighlighter : Node, IController
{
    [Export] public CanvasGroup? Visuals { get; set; }
    [Export] public Color OutlineColor { get; set; }
    [Export(PropertyHint.Range, "1,10")] public int OutlineThickness { get; set; }

    /// <summary>
    /// 节点准备就绪时的回调方法
    /// 在节点添加到场景树后调用
    /// </summary>
    public override void _Ready()
    {
        (Visuals!.Material as ShaderMaterial)?.SetShaderParameter("line_color", OutlineColor);
    }

    /// <summary>
    /// 清除高亮显示效果
    /// </summary>
    /// <remarks>
    /// 通过将着色器参数"line_thickness"设置为0来清除轮廓线效果
    /// </remarks>
    public void ClearHighlight()
    {
        (Visuals!.Material as ShaderMaterial)?.SetShaderParameter("line_thickness", 0);
    }

    /// <summary>
    /// 应用高亮显示效果
    /// </summary>
    /// <remarks>
    /// 通过设置着色器参数"line_thickness"为指定的轮廓厚度来实现高亮效果
    /// </remarks>
    public void Highlight()
    {
        (Visuals!.Material as ShaderMaterial)?.SetShaderParameter("line_thickness", OutlineThickness);
    }
    
}