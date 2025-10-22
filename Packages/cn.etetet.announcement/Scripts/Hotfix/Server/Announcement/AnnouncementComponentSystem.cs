using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(AnnouncementComponent))]
    [FriendOf(typeof(AnnouncementComponent))]
    public static partial class AnnouncementComponentSystem
    {
        [EntitySystem]
        private static void Awake(this AnnouncementComponent self)
        {
            self.Load().NoContext();
        }

        [EntitySystem]
        private static void Destroy(this ET.Server.AnnouncementComponent self)
        {
            foreach (var infos in self.AnnouncementInfoDic.Values)
            {
                foreach (var infoRef in infos)
                {
                    Entity info = infoRef;
                    info?.Dispose();
                }
            }

            self.AnnouncementInfoDic?.Clear();
            self.AnnouncementInfoDic = null;
        }

        private static async ETTask Load(this AnnouncementComponent self)
        {
            self.AnnouncementInfoDic = new Dictionary<int, List<EntityRef<AnnouncementInfo>>>();
            DBComponent dbComponent = self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
            List<AnnouncementInfo> announcementInfos = await dbComponent.Query<AnnouncementInfo>(a => true);
            foreach (var announcementInfo in announcementInfos)
            {
                self.AddAnnouncementInfo(announcementInfo);
                self.AddChild(announcementInfo);
            }
        }

        private static void AddAnnouncementInfo(this AnnouncementComponent self, AnnouncementInfo announcementInfo)
        {
            if (!self.AnnouncementInfoDic.ContainsKey(announcementInfo.GradeClassId))
            {
                self.AnnouncementInfoDic.Add(announcementInfo.GradeClassId, new List<EntityRef<AnnouncementInfo>>());
            }

            var info = self.AnnouncementInfoDic[announcementInfo.GradeClassId].Find(info => (info != null && info.Entity.Id == announcementInfo.Id));
            if (info != null)
            {
                info = announcementInfo;
            }
            else
            {
                self.AnnouncementInfoDic[announcementInfo.GradeClassId].Add(announcementInfo);
            }
        }

        private static async ETTask Save(this AnnouncementComponent self, AnnouncementInfo announcementInfo)
        {
            DBComponent dbComponent = self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
            self.AddAnnouncementInfo(announcementInfo);
            await dbComponent.Save(announcementInfo);
        }

        private static void BroadcastAnnouncement(this AnnouncementComponent self, AnnouncementInfo announcementInfo)
        {
            //TODO 广播公告消息
            MapMessageHelper.Broadcast(self.Root(), announcementInfo.ToMessage());
        }

        public static async ETTask SendAnnouncementMsg(this AnnouncementComponent self, AnnouncementInfoProto announcementInfoProto)
        {
            AnnouncementInfo announcementInfo = self.AddChild<AnnouncementInfo>();
            announcementInfo.FromMessage(announcementInfoProto);
            await self.Save(announcementInfo);
            self.BroadcastAnnouncement(announcementInfo);
        }

        public static async ETTask<bool> EditAnnouncementMsg(this AnnouncementComponent self, AnnouncementInfoProto announcementInfoProto)
        {
            if (self.AnnouncementInfoDic.TryGetValue(announcementInfoProto.GradeClassId, out var value))
            {
                AnnouncementInfo announcementInfo =
                        value.Find(info => (info.Entity != null && info.Entity.Id == announcementInfoProto.Id)).Entity;
                await self.Save(announcementInfo);
                self.BroadcastAnnouncement(announcementInfo);
                return true;
            }

            return false;
        }

        public static List<EntityRef<AnnouncementInfo>> GetAnnouncementInfos(this AnnouncementComponent self, int gradeClassId)
        {
            List<EntityRef<AnnouncementInfo>> announcementInfos;
            if (!self.AnnouncementInfoDic.TryGetValue(gradeClassId, out announcementInfos))
            {
                return null;
            }

            return announcementInfos;
        }
    }
}