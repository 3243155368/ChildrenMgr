namespace ET.Server
{
    /// <summary>
    /// 创建公告消息处理器
    /// </summary>
    [MessageHandler(SceneType.Announcement)]
    public class C2Announcement_CreateAnnouncementHandler : MessageHandler<Scene, C2Announcement_CreateAnnouncement, Announcement2C_CreateAnnouncement>
    {
        protected override async ETTask Run(Scene root, C2Announcement_CreateAnnouncement request, Announcement2C_CreateAnnouncement response)
        {
            // 获取发布者Unit
            Unit publisherUnit = root.GetComponent<UnitComponent>()?.Get(request.PublisherId);
            if (publisherUnit == null)
            {
                response.Error = ErrorCode.ERR_NotFoundOperator;
                response.Message = "未找到发布者";
                return;
            }

            // 检查权限
            if (!AnnouncementPermissionHelper.CheckAnnouncementPermission(publisherUnit))
            {
                response.Error = ErrorCode.ERR_AnnouncementNoPermission;
                response.Message = "您没有发送公告的权限";
                return;
            }

            // 检查内容是否为空
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                response.Error = ErrorCode.ERR_AnnouncementContentEmpty;
                response.Message = "公告内容不能为空";
                return;
            }

            AnnouncementComponent announcementComponent = root.GetComponent<AnnouncementComponent>();
            response.AnnouncementId = await announcementComponent.SendAnnouncementMsg(request.GradeClassId, request.Content, request.PublisherId);
            response.Error = ErrorCode.ERR_Success;
        }
    }
}
