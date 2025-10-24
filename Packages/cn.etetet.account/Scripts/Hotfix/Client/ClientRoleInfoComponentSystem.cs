namespace ET.Client
{
    [EntitySystemOf(typeof(ClientRoleInfoComponent))]
    [FriendOf(typeof(ClientRoleInfoComponent))]
    [FriendOf(typeof(RoleInfo))]
    public static partial class ClientRoleInfoComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ClientRoleInfoComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ClientRoleInfoComponent self)
        {
            self.RoleInfo = null;
        }

        /// <summary>
        /// 获取班级ID
        /// </summary>
        public static int GetGradeClassId(this ClientRoleInfoComponent self)
        {
            return self.RoleInfo?.GradeClassId ?? 0;
        }

        /// <summary>
        /// 获取角色名称
        /// </summary>
        public static string GetName(this ClientRoleInfoComponent self)
        {
            return self.RoleInfo?.Name ?? string.Empty;
        }

        /// <summary>
        /// 从 Proto 消息创建并设置角色信息
        /// </summary>
        public static void SetRoleInfoFromProto(this ClientRoleInfoComponent self, RoleInfoProto proto)
        {
            if (proto == null)
            {
                Log.Error("RoleInfoProto 为空");
                return;
            }

            RoleInfo roleInfo = self.AddChildWithId<RoleInfo>(proto.Id);
            roleInfo.FromMessage(proto);
            self.RoleInfo = roleInfo;
        }
    }
}

