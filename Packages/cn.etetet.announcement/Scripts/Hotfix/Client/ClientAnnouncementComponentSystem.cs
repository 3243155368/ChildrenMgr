namespace ET.Client
{
    [EntitySystemOf(typeof(ClientAnnouncementComponent))]
    [FriendOf(typeof(ClientAnnouncementComponent))]
    public static partial class ClientAnnouncementComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.ClientAnnouncementComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ET.Client.ClientAnnouncementComponent self)
        {
        }

        public static async ETTask SendAnnouncement(this ET.Client.ClientAnnouncementComponent self, int gradeClassId, string content)
        {
            C2Announcement_CreateAnnouncement msg = C2Announcement_CreateAnnouncement.Create();
            msg.Content = content;
            msg.GradeClassId = gradeClassId;
            Announcement2C_CreateAnnouncement createAnnouncement =
                    await self.Root().GetComponent<ClientSenderComponent>().Call(msg) as Announcement2C_CreateAnnouncement;
            
        }
    }
}