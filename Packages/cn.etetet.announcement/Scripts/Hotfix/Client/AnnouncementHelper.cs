namespace ET.Client
{
    public static class AnnouncementHelper
{
    public static async ETTask GetAnnouncements(Unit unit)
    {
        ClientAnnouncementComponent announcementComponent = unit.GetComponent<ClientAnnouncementComponent>();
        await announcementComponent.GetAnnouncements();
    }

    public static async ETTask SendAnnouncement(Unit unit, string content)
    {
        ClientAnnouncementComponent announcementComponent = unit.GetComponent<ClientAnnouncementComponent>();
        await announcementComponent.SendAnnouncement(content);
    }

    public static async ETTask EditAnnouncement(Unit unit, string content, long announcementId)
    {
        ClientAnnouncementComponent announcementComponent = unit.GetComponent<ClientAnnouncementComponent>();
        await announcementComponent.EditAnnouncement(content, announcementId);
    }

    /// <summary>
    /// （通过事件系统调用权限检查）
    /// </summary>
    public static bool CheckAnnouncementPermission(Unit unit)
    {
        bool canSend = EventSystem.Instance.Invoke<CheckAnnouncementPermission, bool>(new CheckAnnouncementPermission { Unit = unit });
        return canSend;
    }
    }
}
