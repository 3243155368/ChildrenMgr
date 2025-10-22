namespace ET
{
    [ChildOf]
    public class AnnouncementInfo:Entity,IAwake
    {
        public int GradeClassId { get; set; } // 班级ID
        public string Content { get; set; } // 公告内容
        public long PublishTime { get; set; } // 发布时间（时间戳）
        public long PublisherId { get; set; } // 发布者（老师ID）
    }
}
