namespace ET
{
    
    public enum RoleInfoState
    {
        Normal = 0,
        Freeze,
    }
    
    [ChildOf]
    public class RoleInfo : Entity,IAwake
    {
        public string Name;

        public int ServerId;

        public int State;

        public string Account;
        
        public int GradeClassId;

        public long LastLoginTime;

        public long CreateTime;
    }
}