using System.Collections.Generic;

namespace ET;

[ComponentOf(typeof(Unit))]
public class AdminComponent : Entity, IAwake,ITransfer, IUnitCache
{
    public Dictionary<long,EntityRef<Announcement>> Announcements;
}