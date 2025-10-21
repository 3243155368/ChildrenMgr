using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AdminComponent : Entity, IAwake, ITransfer, IUnitCache
    {
        public Dictionary<int, List<EntityRef<Announcement>>> Announcements;
    }
}