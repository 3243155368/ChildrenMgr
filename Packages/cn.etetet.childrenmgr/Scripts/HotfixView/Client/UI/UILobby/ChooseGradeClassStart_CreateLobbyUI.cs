using System.Linq;
using YIUIFramework;

namespace ET.Client
{
    [Event(SceneType.ChildrenMgr)]
    public class ChooseGradeClassStart_CreateLobbyUI : AEvent<Scene, ChooseGradeClassStart>
    {
        protected override async ETTask Run(Scene root, ChooseGradeClassStart args)
        {
            int GradeClassId = 0;
            var vo = ParamVo.Get(GradeClassId);
            await root.YIUIRoot()
                    .OpenPanelWaitAsync<NewLobbyPanelComponent, int[], ParamVo>(args.RoleInfo.Select(info => info.GradeId).ToArray(), vo);
            GradeClassId = vo.Get<int>();
            if (GradeClassId > 0)
            {
                await LoginHelper.CreateRole(root, args.Account, args.Token, args.ServerId, GradeClassId, args.RoleInfo);
            }
            else
            {
                Log.Error("选择班级失败");
            }
            ParamVo.Put(vo);
        }
    }
}