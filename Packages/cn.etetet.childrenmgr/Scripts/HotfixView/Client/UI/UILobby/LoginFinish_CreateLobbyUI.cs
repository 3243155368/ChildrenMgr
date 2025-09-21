
namespace ET.Client
{
	[Event(SceneType.ChildrenMgr)]
	public class LoginFinish_CreateLobbyUI: AEvent<Scene, LoginFinish>
	{
		protected override async ETTask Run(Scene root, LoginFinish args)
		{
			await root.YIUIRoot().OpenPanelAsync<NewLobbyPanelComponent>();
		}
	}
}
