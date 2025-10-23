namespace ET.Server
{
    /// <summary>
    /// 标记公告已读消息处理器
    /// </summary>
    [MessageHandler(SceneType.Announcement)]
    [FriendOf(typeof(AnnouncementComponent))]
    public class C2Announcement_RedirectAnnouncementHandler : MessageHandler<Scene, C2Announcement_RedirectAnnouncement, Announcement2C_RedirectAnnouncement>
    {
        protected override async ETTask Run(Scene root, C2Announcement_RedirectAnnouncement request, Announcement2C_RedirectAnnouncement response)
        {
            AnnouncementComponent announcementComponent = root.GetComponent<AnnouncementComponent>();

            // 使用请求中的GradeClassId直接定位公告
            bool success = await announcementComponent.RedirectAnnouncementMsg(request.AnnouncementId, request.RedirectUserId, request.GradeClassId);

            if (success)
            {
                response.Error = ErrorCode.ERR_Success;
            }
            else
            {
                response.Error = ErrorCode.ERR_NotFoundAnnouncement;
                response.Message = "未找到指定的公告";
            }
        }
    }
}

