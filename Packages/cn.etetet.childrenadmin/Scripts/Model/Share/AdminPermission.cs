namespace ET
{
    /// <summary>
    /// 管理员权限定义（前后端共享）
    /// </summary>
    public static class AdminPermission
    {
        public const long ViewAnnouncement = 1L << 0;      // 查看公告
        public const long CreateAnnouncement = 1L << 1;    // 创建公告
        public const long EditAnnouncement = 1L << 2;      // 编辑公告
        public const long DeleteAnnouncement = 1L << 3;    // 删除公告
        public const long ManageUser = 1L << 4;            // 管理用户
        public const long ViewStatistics = 1L << 5;        // 查看统计

        // 预定义权限组
        public const long BasicAdmin = ViewAnnouncement | CreateAnnouncement;
        public const long AdvancedAdmin = BasicAdmin | EditAnnouncement | DeleteAnnouncement;
        public const long SuperAdmin = -1; // 所有权限
    }
}

