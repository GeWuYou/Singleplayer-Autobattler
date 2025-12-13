using GFramework.Core.architecture;
using GFramework.Core.Godot.component;
using SingleplayerAutobattler.scripts.architecture;

namespace SingleplayerAutobattler.scripts.component;

/// <summary>
/// 拖拽组件类，用于处理节点的拖放逻辑。
/// 实现了 IController 接口以支持架构通信，并通过信号通知拖拽事件的发生。
/// </summary>
public partial class DragDropComponent : AbstractDragDropArea2DComponent
{
	public override IArchitecture GetArchitecture()=> GameArchitecture.Instance;
	
	public override void _Ready()
	{
		base._Ready();
		Enable = true;
	}
}