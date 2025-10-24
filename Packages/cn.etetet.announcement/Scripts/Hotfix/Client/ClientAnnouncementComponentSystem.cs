using System.Collections.Generic;

namespace ET.Client
{
    [EntitySystemOf(typeof(ClientAnnouncementComponent))]
    [FriendOf(typeof(ClientAnnouncementComponent))]
    public static partial class ClientAnnouncementComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ClientAnnouncementComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ClientAnnouncementComponent self)
        {
        }
        public static async ETTask GetAnnouncements(this ClientAnnouncementComponent self)
        {
            self.AnnouncementInfoList = new List<EntityRef<AnnouncementInfo>>();
            C2Announcement_GetAnnouncements msg = C2Announcement_GetAnnouncements.Create();
            msg.GradeClassId = self.Root().GetComponent<ClientRoleInfoComponent>().GetGradeClassId();
            Announcement2C_GetAnnouncements getAnnouncements =
                    await self.Root().GetComponent<ClientSenderComponent>().Call(msg) as Announcement2C_GetAnnouncements;
            foreach (var infoProto in getAnnouncements.AnnouncementInfos)
            {
                AnnouncementInfo announcementInfo = self.AddChildWithId<AnnouncementInfo>(infoProto.Id);
                self.AnnouncementInfoList.Add(announcementInfo);
            }
        }

        public static async ETTask SendAnnouncement(this ClientAnnouncementComponent self, string content)
        {
            if (!AnnouncementHelper.CheckAnnouncementPermission(self.GetParent<Unit>()))
            {
                return;
            }
            C2Announcement_CreateAnnouncement msg = C2Announcement_CreateAnnouncement.Create();
            msg.Content = content;
            msg.PublisherId = self.Root().GetComponent<PlayerComponent>().MyId;
            msg.GradeClassId = self.Root().GetComponent<ClientRoleInfoComponent>().GetGradeClassId();
            Announcement2C_CreateAnnouncement createAnnouncement =
                    await self.Root().GetComponent<ClientSenderComponent>().Call(msg) as Announcement2C_CreateAnnouncement;
        }

        public static async ETTask EditAnnouncement(this ClientAnnouncementComponent self, string content,long announcementId)
        {
            if (!AnnouncementHelper.CheckAnnouncementPermission(self.GetParent<Unit>()))
            {
                return;
            }
            C2Announcement_EditAnnouncement msg = C2Announcement_EditAnnouncement.Create();
            msg.Content = content;
            msg.AnnouncementId = announcementId;
            msg.GradeClassId = self.Root().GetComponent<ClientRoleInfoComponent>().GetGradeClassId();
            Announcement2C_EditAnnouncement editAnnouncement =
                    await self.Root().GetComponent<ClientSenderComponent>().Call(msg) as Announcement2C_EditAnnouncement;
        }

        public static async ETTask DeleteAnnouncement(this ClientAnnouncementComponent self, long announcementId)
        {
            if (!AnnouncementHelper.CheckAnnouncementPermission(self.GetParent<Unit>()))
            {
                return;
            }
            C2Announcement_DeleteAnnouncement msg = C2Announcement_DeleteAnnouncement.Create();
            msg.AnnouncementId = announcementId;
            Announcement2C_DeleteAnnouncement deleteAnnouncement =
                    await self.Root().GetComponent<ClientSenderComponent>().Call(msg) as Announcement2C_DeleteAnnouncement;
        }

        /// <summary>
        /// 标记公告为已读
        /// </summary>
        /// <param name="self">客户端公告组件</param>
        /// <param name="announcementId">公告ID</param>
        public static async ETTask RedirectAnnouncement(this ClientAnnouncementComponent self, long announcementId)
        {
            C2Announcement_RedirectAnnouncement msg = C2Announcement_RedirectAnnouncement.Create();
            msg.AnnouncementId = announcementId;
            msg.RedirectUserId = self.Root().GetComponent<PlayerComponent>().MyId;
            msg.GradeClassId = self.Root().GetComponent<ClientRoleInfoComponent>().GetGradeClassId();
            Announcement2C_RedirectAnnouncement redirectAnnouncement =
                    await self.Root().GetComponent<ClientSenderComponent>().Call(msg) as Announcement2C_RedirectAnnouncement;
        }
    }
}
