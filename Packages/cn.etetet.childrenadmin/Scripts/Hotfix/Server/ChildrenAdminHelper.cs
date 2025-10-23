using System.Collections.Generic;

namespace ET.Server
{
    /// <summary>
    /// 管理员权限定义
    /// </summary>
    public static class AdminPermission
    {
        public const long ViewAnnouncement = 1L << 0;      // 查看公告
        public const long CreateAnnouncement = 1L << 1;    // 创建公告
        public const long EditAnnouncement = 1L << 2;      // 编辑公告
        public const long DeleteAnnouncement = 1L << 3;    // 删除公告
        public const long ManageUser = 1L << 4;            // 管理用户
        public const long ViewStatistics = 1L << 5;        // 查看统计

        // 预定义权限组
        public const long BasicAdmin = ViewAnnouncement | CreateAnnouncement;
        public const long AdvancedAdmin = BasicAdmin | EditAnnouncement | DeleteAnnouncement;
        public const long SuperAdmin = -1; // 所有权限
    }

    /// <summary>
    /// 儿童管理员辅助类
    /// </summary>
    [FriendOf(typeof(ChildrenAdminComponent))]
    public static class ChildrenAdminHelper
    {
        /// <summary>
        /// 检查Unit是否为管理员
        /// </summary>
        public static bool IsAdmin(Unit unit)
        {
            return unit?.GetComponent<ChildrenAdminComponent>() != null;
        }

        /// <summary>
        /// 检查Unit是否有指定权限
        /// </summary>
        public static bool CheckPermission(Unit unit, long permission)
        {
            ChildrenAdminComponent adminComponent = unit?.GetComponent<ChildrenAdminComponent>();
            if (adminComponent == null)
            {
                return false;
            }

            return adminComponent.HasPermission(permission);
        }

        /// <summary>
        /// 为Unit添加管理员组件
        /// </summary>
        public static ChildrenAdminComponent AddAdmin(Unit unit, int adminLevel = 1, long permissions = 0)
        {
            if (unit == null)
            {
                Log.Error("Unit为空，无法添加管理员组件");
                return null;
            }

            // 如果已经是管理员，则更新权限
            ChildrenAdminComponent adminComponent = unit.GetComponent<ChildrenAdminComponent>();
            if (adminComponent != null)
            {
                adminComponent.PermissionFlags = permissions;
                return adminComponent;
            }

            // 添加新的管理员组件
            adminComponent = unit.AddComponent<ChildrenAdminComponent>();
            adminComponent.PermissionFlags = permissions;

            Log.Info($"为Unit {unit.Id} 添加儿童管理员组件，等级: {adminLevel}, 权限: {permissions}");

            return adminComponent;
        }

        /// <summary>
        /// 移除Unit的管理员身份
        /// </summary>
        public static void RemoveAdmin(Unit unit)
        {
            if (unit == null)
            {
                return;
            }

            ChildrenAdminComponent adminComponent = unit.GetComponent<ChildrenAdminComponent>();
            if (adminComponent != null)
            {
                unit.RemoveComponent<ChildrenAdminComponent>();
                Log.Info($"已移除Unit {unit.Id} 的儿童管理员身份");
            }
        }

        /// <summary>
        /// 从数据库加载管理员信息（示例）
        /// </summary>
        public static async ETTask<bool> LoadAdminFromDB(Scene scene, Unit unit)
        {
            // TODO: 从数据库查询该Unit是否为管理员
            // 这里需要根据实际的数据库结构实现

            // 示例代码：
            // DBComponent dbComponent = scene.Root().GetComponent<DBManagerComponent>().GetZoneDB(scene.Zone());
            // var adminInfo = await dbComponent.Query<AdminInfo>(a => a.UnitId == unit.Id);
            // if (adminInfo != null && adminInfo.Count > 0)
            // {
            //     AddAdmin(unit, adminInfo[0].AdminLevel, adminInfo[0].Permissions);
            //     return true;
            // }

            await ETTask.CompletedTask;
            return false;
        }

        /// <summary>
        /// 获取所有在线管理员
        /// </summary>
        public static List<Unit> GetOnlineAdmins(Scene scene)
        {
            List<Unit> admins = new List<Unit>();
            List<Unit> allUnits = new List<Unit>();
            scene.GetComponent<UnitComponent>()?.GetAll(allUnits);

            foreach (Unit unit in allUnits)
            {
                if (IsAdmin(unit))
                {
                    admins.Add(unit);
                }
            }

            return admins;
        }
    }
}

