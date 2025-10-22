using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class ClientAnnouncementComponent:Entity, IAwake,IDestroy
    {
        public List<EntityRef<AnnouncementInfo>> AnnouncementInfoList;
    }
}
