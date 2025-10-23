namespace ET.Server
{
    /// <summary>
    /// 编辑公告消息处理器
    /// </summary>
    [MessageHandler(SceneType.Announcement)]
    public class C2Announcement_EditAnnouncementHandler : MessageHandler<Scene, C2Announcement_EditAnnouncement, Announcement2C_EditAnnouncement>
    {
        protected override async ETTask Run(Scene root, C2Announcement_EditAnnouncement request, Announcement2C_EditAnnouncement response)
        {
            AnnouncementComponent announcementComponent = root.GetComponent<AnnouncementComponent>();

            bool success = await announcementComponent.EditAnnouncementMsg(request.AnnouncementId, request.GradeClassId, request.Content);

            if (success)
            {
                response.AnnouncementId = request.AnnouncementId;
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

