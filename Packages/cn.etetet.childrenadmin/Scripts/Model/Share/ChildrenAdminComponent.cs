namespace ET
{
    /// <summary>
    /// 儿童管理员组件（前后端共享）
    /// </summary>
    [ComponentOf(typeof(Unit))]
    public class ChildrenAdminComponent : Entity, IAwake, IDestroy
    {
        /// <summary>
        /// 管理员等级
        /// </summary>
        public int AdminLevel { get; set; }

        /// <summary>
        /// 管理员权限标识
        /// </summary>
        public long PermissionFlags { get; set; }

        /// <summary>
        /// 是否为超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }
    }
}

