namespace ET.Client
{
    /// <summary>
    /// 检查公告权限调用器 - 基于管理员组件判断
    /// </summary>
    [Invoke]
    public class CheckAnnouncementPermissionInvoker : AInvokeHandler<CheckAnnouncementPermission, bool>
    {
        public override bool Handle(CheckAnnouncementPermission args)
        {
            // 检查Unit是否有ChildrenAdminComponent
            ChildrenAdminComponent adminComponent = args.Unit?.GetComponent<ChildrenAdminComponent>();

            if (adminComponent == null)
            {
                // 没有管理员组件，不能发送公告
                return false;
            }

            // 检查是否有创建公告的权限
            return adminComponent.HasPermission(AdminPermission.CreateAnnouncement) || adminComponent.IsSuperAdmin;
        }
    }
}

