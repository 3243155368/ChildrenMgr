using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class AnnouncementComponent : Entity, IAwake,IDestroy
    {
        public Dictionary<int, List<EntityRef<AnnouncementInfo>>> AnnouncementInfoDic;
    }
}