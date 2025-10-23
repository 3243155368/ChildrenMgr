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
        private static void Destroy(this AnnouncementComponent self)
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

        /// <summary>
        /// 发送公告消息
        /// </summary>
        /// <param name="self">公告组件</param>
        /// <param name="gradeClassId">班级ID</param>
        /// <param name="content">公告内容</param>
        /// <param name="unitId">发布者ID</param>
        /// <returns>公告ID</returns>
        public static async ETTask<long> SendAnnouncementMsg(this AnnouncementComponent self, int gradeClassId, string content, long unitId)
        {
            AnnouncementInfo announcementInfo = self.AddChild<AnnouncementInfo>();
            announcementInfo.GradeClassId = gradeClassId;
            announcementInfo.Content = content;
            announcementInfo.PublishTime = TimeInfo.Instance.ServerNow();
            announcementInfo.PublisherId = unitId;

            // 发布人（管理员）自动标记为已读
            announcementInfo.RedirectStudentIds = new List<long> { unitId };

            await self.Save(announcementInfo);
            self.BroadcastAnnouncement(announcementInfo);
            return announcementInfo.Id;
        }

        public static async ETTask<bool> EditAnnouncementMsg(this AnnouncementComponent self, long announcementId, int gradeClassId, string content)
        {
            if (self.AnnouncementInfoDic.TryGetValue(gradeClassId, out var value))
            {
                EntityRef<AnnouncementInfo> infoRef = value.Find(info => (info != null && info.Entity != null && info.Entity.Id == announcementId));
                AnnouncementInfo announcementInfo = infoRef;
                if (announcementInfo is { IsDisposed: false })
                {
                    announcementInfo.Content = content;
                    await self.Save(announcementInfo);
                    self.BroadcastAnnouncement(announcementInfo);
                    return true;
                }
            }

            return false;
        }

        public static async ETTask<bool> DeleteAnnouncementMsg(this AnnouncementComponent self, long announcementId, int gradeClassId)
        {
            if (self.AnnouncementInfoDic.TryGetValue(gradeClassId, out var value))
            {
                EntityRef<AnnouncementInfo> infoRef = value.Find(info => (info != null && info.Entity != null && info.Entity.Id == announcementId));
                AnnouncementInfo announcementInfo = infoRef;
                if (announcementInfo is { IsDisposed: false })
                {
                    announcementInfo.State = (int)AnnouncementState.Freeze;
                    await self.Save(announcementInfo);
                    self.BroadcastAnnouncement(announcementInfo);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 标记公告为已读（添加用户到已读列表）
        /// </summary>
        /// <param name="self">公告组件</param>
        /// <param name="announcementId">公告ID</param>
        /// <param name="redirectUserId">已读用户ID</param>
        /// <param name="gradeClassId">班级ID</param>
        /// <returns>是否找到并处理成功（已读重复添加也返回true）</returns>
        public static async ETTask<bool> RedirectAnnouncementMsg(this AnnouncementComponent self, long announcementId, long redirectUserId, int gradeClassId)
        {
            if (self.AnnouncementInfoDic.TryGetValue(gradeClassId, out var value))
            {
                EntityRef<AnnouncementInfo> infoRef = value.Find(info => (info != null && info.Entity != null && info.Entity.Id == announcementId));
                AnnouncementInfo announcementInfo = infoRef;
                if (announcementInfo is { IsDisposed: false })
                {
                    announcementInfo.RedirectStudentIds ??= new List<long>();

                    // 如果已经标记过已读，直接返回成功，不需要重复保存
                    if (announcementInfo.RedirectStudentIds.Contains(redirectUserId))
                    {
                        return true;
                    }

                    // 添加到已读列表并保存
                    announcementInfo.RedirectStudentIds.Add(redirectUserId);
                    await self.Save(announcementInfo);
                    self.BroadcastAnnouncement(announcementInfo);
                    return true;
                }
            }

            return false;
        }

        public static List<AnnouncementInfo> GetAnnouncementInfos(this AnnouncementComponent self, int gradeClassId)
        {
            if (!self.AnnouncementInfoDic.TryGetValue(gradeClassId, out var value))
            {
                return null;
            }
            List<AnnouncementInfo> announcementInfos = new List<AnnouncementInfo>(value.Count);
            foreach (var announcementInfo in self.AnnouncementInfoDic[gradeClassId])
            {
                if (announcementInfo.Entity is { State: (int)AnnouncementState.Normal })
                {
                    announcementInfos.Add(announcementInfo);
                }
            }
            return announcementInfos;
        }
    }
}
