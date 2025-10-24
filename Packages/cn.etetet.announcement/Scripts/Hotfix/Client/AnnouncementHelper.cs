namespace ET.Client
{
    /// <summary>
    /// 公告客户端辅助类
    /// 提供便捷的公告操作接口
    /// </summary>
    public static class AnnouncementHelper
    {
        /// <summary>
        /// 检查公告权限（通过事件系统调用权限检查）
        /// </summary>
        public static bool CheckAnnouncementPermission(Unit unit)
        {
            bool canSend = EventSystem.Instance.Invoke<CheckAnnouncementPermission, bool>(new CheckAnnouncementPermission { Unit = unit });
            return canSend;
        }
    }
}
