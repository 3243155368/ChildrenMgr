namespace ET
{
    /// <summary>
    /// 公告系统错误码定义
    /// </summary>
    public static partial class ErrorCode
    {
        // 公告相关错误码 (200000000 + 202 * 1000 + X)
        // 200000000以上不抛异常，需要自己判断处理

        /// <summary>
        /// 未找到指定的公告
        /// </summary>
        public const int ERR_NotFoundAnnouncement = ERR_WithoutException + PackageType.Announcement * 1000 + 1;

        /// <summary>
        /// 公告已被删除或冻结
        /// </summary>
        public const int ERR_AnnouncementDeleted = ERR_WithoutException + PackageType.Announcement * 1000 + 2;

        /// <summary>
        /// 没有权限操作该公告
        /// </summary>
        public const int ERR_AnnouncementNoPermission = ERR_WithoutException + PackageType.Announcement * 1000 + 3;

        /// <summary>
        /// 公告内容为空
        /// </summary>
        public const int ERR_AnnouncementContentEmpty = ERR_WithoutException + PackageType.Announcement * 1000 + 4;

        /// <summary>
        /// 未找到操作者
        /// </summary>
        public const int ERR_NotFoundOperator = ERR_WithoutException + PackageType.Announcement * 1000 + 5;
    }
}

