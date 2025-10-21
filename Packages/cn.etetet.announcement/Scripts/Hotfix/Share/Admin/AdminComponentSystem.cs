using System.Collections.Generic;
using ET.Server;

namespace ET
{
    [EntitySystemOf(typeof(AdminComponent))]
    [FriendOf(typeof(AdminComponent))]
    public static partial class AdminComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.AdminComponent self)
        {
        
        }

        private static async ETTask Load(this ET.AdminComponent self)
        {
            CoroutineLockComponent  coroutineLockComponent = self.Root().GetComponent<CoroutineLockComponent>();
            using (await coroutineLockComponent.Wait(CoroutineLockType.GetAnnouncement,self.GetParent<Unit>().Id))
            {
                DBComponent dbComponent = self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
                //List<Announcement> roleInfos = await dbComponent.Query<Announcement>`
            }
        }
    }
}
