namespace ET
{
    [EntitySystemOf(typeof(AnnouncementInfo))]
    [FriendOf(typeof(AnnouncementInfo))]
    public static partial class AnnouncementInfoSystem
    {
        [EntitySystem]
        private static void Awake(this ET.AnnouncementInfo self)
        {

        }
        public static void FromMessage(this AnnouncementInfo self, AnnouncementInfoProto announcementInfoProto)
        {
            self.GradeClassId = announcementInfoProto.GradeClassId;
            self.Content = announcementInfoProto.Content;
            self.PublisherId = announcementInfoProto.PublisherId;
            self.PublishTime = announcementInfoProto.PublishTime;
        }

        public static AnnouncementInfoProto ToMessage(this AnnouncementInfo self)
        {
            AnnouncementInfoProto announcementInfoProto = AnnouncementInfoProto.Create();
            announcementInfoProto.GradeClassId = self.GradeClassId;
            announcementInfoProto.Content = self.Content;
            announcementInfoProto.PublisherId = self.PublisherId;
            announcementInfoProto.PublishTime = self.PublishTime;
            return announcementInfoProto;
        }
    }
}