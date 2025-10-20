using System.Collections.Generic;

namespace ET
{
    public struct LoginFinish
    {

    }
    public struct ChooseGradeClassStart
    {
        public List<RoleInfoProto> RoleInfo
        {
            get;
            set;
        }

        public string Account
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        public int ServerId
        {
            get;
            set;
        }
    }
}