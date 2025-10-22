namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class ClientRoleInfoComponent : Entity, IAwake
    {
        private EntityRef<RoleInfo> RoleInfoRef;

        public RoleInfo RoleInfo
        {
            get => this.RoleInfoRef;
            set => this.RoleInfoRef = value;
        }
    }
}