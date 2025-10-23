namespace ET
{
    /// <summary>
    /// 儿童管理员组件System（前后端共享）
    /// </summary>
    [EntitySystemOf(typeof(ChildrenAdminComponent))]
    [FriendOf(typeof(ChildrenAdminComponent))]
    public static partial class ChildrenAdminComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ChildrenAdminComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ChildrenAdminComponent self)
        {

        }
        /// <summary>
        /// 检查是否有指定权限
        /// </summary>
        public static bool HasPermission(this ChildrenAdminComponent self, long permission)
        {
            if (self.IsSuperAdmin)
            {
                return true; // 超级管理员拥有所有权限
            }

            return (self.PermissionFlags & permission) != 0;
        }
    }
}

