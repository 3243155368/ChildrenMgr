namespace ET.Server
{
    [MessageHandler(SceneType.Announcement)]
    public class C2Announcement_CreateAnnouncementHandler : MessageHandler<Scene, C2Announcement_CreateAnnouncement, Announcement2C_CreateAnnouncement>
    {
        protected override async ETTask Run(Scene root, C2Announcement_CreateAnnouncement request, Announcement2C_CreateAnnouncement response)
        {
            AnnouncementComponent announcementComponent = root.GetComponent<AnnouncementComponent>();
            response.AnnouncementId = await announcementComponent.SendAnnouncementMsg(request.GradeClassId,request.Content,request.PublisherId);
            response.Error = ErrorCode.ERR_Success;
        }
    }
}
