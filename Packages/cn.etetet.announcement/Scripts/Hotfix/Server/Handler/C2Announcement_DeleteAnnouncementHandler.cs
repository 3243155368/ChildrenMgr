namespace ET.Server
{
    /// <summary>
    /// 删除公告消息处理器
    /// </summary>
    [MessageHandler(SceneType.Announcement)]
    [FriendOf(typeof(AnnouncementComponent))]
    public class C2Announcement_DeleteAnnouncementHandler : MessageHandler<Scene, C2Announcement_DeleteAnnouncement, Announcement2C_DeleteAnnouncement>
    {
        protected override async ETTask Run(Scene root, C2Announcement_DeleteAnnouncement request, Announcement2C_DeleteAnnouncement response)
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
                response.Message = "您没有删除公告的权限";
                return;
            }

            AnnouncementComponent announcementComponent = root.GetComponent<AnnouncementComponent>();

            // 遍历所有班级查找该公告并删除
            bool success = false;
            foreach (var kvp in announcementComponent.AnnouncementInfoDic)
            {
                int gradeClassId = kvp.Key;
                success = await announcementComponent.DeleteAnnouncementMsg(request.AnnouncementId, gradeClassId);
                if (success)
                {
                    break;
                }
            }

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

