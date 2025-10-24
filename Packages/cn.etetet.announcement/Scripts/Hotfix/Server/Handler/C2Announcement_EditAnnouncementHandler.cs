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
            // 获取操作者Unit
            Unit operatorUnit = root.GetComponent<UnitComponent>()?.Get(request.PublisherId);
            if (operatorUnit == null)
            {
                response.Error = ErrorCode.ERR_NotFoundOperator;
                response.Message = "未找到操作者";
                return;
            }

            // 检查权限
            if (!AnnouncementPermissionHelper.CheckAnnouncementPermission(operatorUnit))
            {
                response.Error = ErrorCode.ERR_AnnouncementNoPermission;
                response.Message = "您没有编辑公告的权限";
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

