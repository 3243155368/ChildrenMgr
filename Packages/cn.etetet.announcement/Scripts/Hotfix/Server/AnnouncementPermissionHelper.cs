namespace ET.Server
{
    /// <summary>
    /// 公告权限检查辅助类（服务端）
    /// </summary>
    public static class AnnouncementPermissionHelper
    {
        /// <summary>
        /// 检查是否有公告权限（通过事件系统调用权限检查）
        /// </summary>
        public static bool CheckAnnouncementPermission(Unit unit)
        {
            bool canSend = EventSystem.Instance.Invoke<CheckAnnouncementPermission, bool>(new CheckAnnouncementPermission { Unit = unit });
            return canSend;
        }
    }
}

