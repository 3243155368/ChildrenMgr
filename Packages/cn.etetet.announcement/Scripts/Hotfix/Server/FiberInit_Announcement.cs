namespace ET.Server
{
    [Invoke((long) SceneType.Announcement)]
    public class FiberInit_Announcement:AInvokeHandler<FiberInit,ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<MailBoxComponent, int>(MailBoxType.UnOrderedMessage);
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<MessageSender>();
            root.AddComponent<LocationProxyComponent>();
            root.AddComponent<MessageLocationSenderComponent>();
            
            root.AddComponent<DBManagerComponent>();
            
            root.AddComponent<AnnouncementComponent>();
            await ETTask.CompletedTask;
        }
    }
}