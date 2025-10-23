using System.Collections.Generic;

namespace ET
{
    public enum AnnouncementState
    {
        Normal = 0,
        Freeze,
    }
    [ChildOf]
    public class AnnouncementInfo:Entity,IAwake
    {
        public int GradeClassId { get; set; } // 班级ID
        public string Content { get; set; } // 公告内容
        public long PublishTime { get; set; } // 发布时间（时间戳）
        public long PublisherId { get; set; } // 发布者（老师ID）
        public int State { get; set; } // 公告状态
        
        public List<long> RedirectStudentIds { get; set; } // 已读学生ID列表
    }
}
