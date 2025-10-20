namespace ET;

[ChildOf]
public class Announcement:Entity
{
    public long AnnouncementId { get; set; } // 唯一标识（可自增或用时间戳）
    public string Content { get; set; } // 公告内容
    public long PublishTime { get; set; } // 发布时间（时间戳）
    public long TeacherId { get; set; } // 发布者（老师ID）
}