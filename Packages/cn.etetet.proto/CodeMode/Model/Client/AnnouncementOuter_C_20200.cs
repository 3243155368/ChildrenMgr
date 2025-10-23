using MemoryPack;
using System.Collections.Generic;

namespace ET
{
    [MemoryPackable]
    [Message(AnnouncementOuter.AnnouncementInfoProto)]
    public partial class AnnouncementInfoProto : MessageObject
    {
        public static AnnouncementInfoProto Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<AnnouncementInfoProto>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public long Id { get; set; }

        [MemoryPackOrder(2)]
        public int GradeClassId { get; set; }

        [MemoryPackOrder(1)]
        public string Content { get; set; }

        [MemoryPackOrder(4)]
        public long PublishTime { get; set; }

        [MemoryPackOrder(5)]
        public long PublisherId { get; set; }

        [MemoryPackOrder(6)]
        public int State { get; set; }

        [MemoryPackOrder(7)]
        public List<long> RedirectStudentIds { get; set; } = new();

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.Id = default;
            this.GradeClassId = default;
            this.Content = default;
            this.PublishTime = default;
            this.PublisherId = default;
            this.State = default;
            this.RedirectStudentIds.Clear();

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.C2Announcement_GetAnnouncements)]
    [ResponseType(nameof(Announcement2C_GetAnnouncements))]
    public partial class C2Announcement_GetAnnouncements : MessageObject, IAnnouncementInfoRequest
    {
        public static C2Announcement_GetAnnouncements Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<C2Announcement_GetAnnouncements>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(0)]
        public int GradeClassId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.GradeClassId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.Announcement2C_GetAnnouncements)]
    public partial class Announcement2C_GetAnnouncements : MessageObject, IAnnouncementInfoResponse
    {
        public static Announcement2C_GetAnnouncements Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<Announcement2C_GetAnnouncements>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(90)]
        public int Error { get; set; }

        [MemoryPackOrder(91)]
        public string Message { get; set; }

        [MemoryPackOrder(0)]
        public List<AnnouncementInfoProto> AnnouncementInfos { get; set; } = new();

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.AnnouncementInfos.Clear();

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.C2Announcement_CreateAnnouncement)]
    [ResponseType(nameof(Announcement2C_CreateAnnouncement))]
    public partial class C2Announcement_CreateAnnouncement : MessageObject, IAnnouncementInfoRequest
    {
        public static C2Announcement_CreateAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<C2Announcement_CreateAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(0)]
        public long PublisherId { get; set; }

        [MemoryPackOrder(1)]
        public int GradeClassId { get; set; }

        [MemoryPackOrder(2)]
        public string Content { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.PublisherId = default;
            this.GradeClassId = default;
            this.Content = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.Announcement2C_CreateAnnouncement)]
    public partial class Announcement2C_CreateAnnouncement : MessageObject, IAnnouncementInfoResponse
    {
        public static Announcement2C_CreateAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<Announcement2C_CreateAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(90)]
        public int Error { get; set; }

        [MemoryPackOrder(91)]
        public string Message { get; set; }

        [MemoryPackOrder(0)]
        public long AnnouncementId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.AnnouncementId = default;

            ObjectPool.Recycle(this);
        }
    }

    // 重新编辑文本公告
    [MemoryPackable]
    [Message(AnnouncementOuter.C2Announcement_EditAnnouncement)]
    [ResponseType(nameof(Announcement2C_EditAnnouncement))]
    public partial class C2Announcement_EditAnnouncement : MessageObject, IAnnouncementInfoRequest
    {
        public static C2Announcement_EditAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<C2Announcement_EditAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(0)]
        public long AnnouncementId { get; set; }

        [MemoryPackOrder(1)]
        public int GradeClassId { get; set; }

        [MemoryPackOrder(2)]
        public string Content { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AnnouncementId = default;
            this.GradeClassId = default;
            this.Content = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.Announcement2C_EditAnnouncement)]
    public partial class Announcement2C_EditAnnouncement : MessageObject, IAnnouncementInfoResponse
    {
        public static Announcement2C_EditAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<Announcement2C_EditAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(90)]
        public int Error { get; set; }

        [MemoryPackOrder(91)]
        public string Message { get; set; }

        [MemoryPackOrder(0)]
        public long AnnouncementId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.AnnouncementId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.C2Announcement_DeleteAnnouncement)]
    [ResponseType(nameof(Announcement2C_DeleteAnnouncement))]
    public partial class C2Announcement_DeleteAnnouncement : MessageObject, IAnnouncementInfoRequest
    {
        public static C2Announcement_DeleteAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<C2Announcement_DeleteAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(0)]
        public long AnnouncementId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AnnouncementId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.Announcement2C_DeleteAnnouncement)]
    public partial class Announcement2C_DeleteAnnouncement : MessageObject, IAnnouncementInfoResponse
    {
        public static Announcement2C_DeleteAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<Announcement2C_DeleteAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(90)]
        public int Error { get; set; }

        [MemoryPackOrder(91)]
        public string Message { get; set; }

        [MemoryPackOrder(0)]
        public long AnnouncementId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.AnnouncementId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.C2Announcement_RedirectAnnouncement)]
    [ResponseType(nameof(Announcement2C_RedirectAnnouncement))]
    public partial class C2Announcement_RedirectAnnouncement : MessageObject, IAnnouncementInfoRequest
    {
        public static C2Announcement_RedirectAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<C2Announcement_RedirectAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(0)]
        public long AnnouncementId { get; set; }

        [MemoryPackOrder(1)]
        public int GradeClassId { get; set; }

        [MemoryPackOrder(2)]
        public long RedirectUserId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AnnouncementId = default;
            this.GradeClassId = default;
            this.RedirectUserId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.Announcement2C_RedirectAnnouncement)]
    public partial class Announcement2C_RedirectAnnouncement : MessageObject, IAnnouncementInfoResponse
    {
        public static Announcement2C_RedirectAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<Announcement2C_RedirectAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(90)]
        public int Error { get; set; }

        [MemoryPackOrder(91)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(AnnouncementOuter.Announcement2C_BroadcastAnnouncement)]
    public partial class Announcement2C_BroadcastAnnouncement : MessageObject, IAnnouncementInfoMessage
    {
        public static Announcement2C_BroadcastAnnouncement Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<Announcement2C_BroadcastAnnouncement>(isFromPool);
        }

        [MemoryPackOrder(89)]
        public int RpcId { get; set; }

        [MemoryPackOrder(0)]
        public AnnouncementInfoProto AnnouncementInfos { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AnnouncementInfos = default;

            ObjectPool.Recycle(this);
        }
    }

    public static class AnnouncementOuter
    {
        public const ushort AnnouncementInfoProto = 20201;
        public const ushort C2Announcement_GetAnnouncements = 20202;
        public const ushort Announcement2C_GetAnnouncements = 20203;
        public const ushort C2Announcement_CreateAnnouncement = 20204;
        public const ushort Announcement2C_CreateAnnouncement = 20205;
        public const ushort C2Announcement_EditAnnouncement = 20206;
        public const ushort Announcement2C_EditAnnouncement = 20207;
        public const ushort C2Announcement_DeleteAnnouncement = 20208;
        public const ushort Announcement2C_DeleteAnnouncement = 20209;
        public const ushort C2Announcement_RedirectAnnouncement = 20210;
        public const ushort Announcement2C_RedirectAnnouncement = 20211;
        public const ushort Announcement2C_BroadcastAnnouncement = 20212;
    }
}