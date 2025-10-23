namespace ET.Server
{
    /// <summary>
    /// Unit创建事件监听器 - 为管理员Unit添加ChildrenAdminComponent
    /// </summary>
    [Event(SceneType.Map)]
    public class UnitCreateEvent_AddChildrenAdminComponent : AEvent<Scene, UnitCreate>
    {
        protected override async ETTask Run(Scene scene, UnitCreate args)
        {
            Unit unit = args.Unit;

            // 这里可以根据业务逻辑判断是否需要添加ChildrenAdminComponent
            // 例如：从数据库查询该Unit是否为管理员

            // 示例：暂时为所有玩家类型的Unit添加ChildrenAdminComponent（实际应该根据业务判断）
            if (args.UnitType == UnitType.Player)
            {
                // 检查Unit是否已经有ChildrenAdminComponent
                if (unit.GetComponent<ChildrenAdminComponent>() == null)
                {
                    // TODO: 这里应该从数据库或配置中加载管理员信息
                    // 临时设置为高级管理员，拥有所有权限
                    ChildrenAdminHelper.AddAdmin(unit, adminLevel: 1, AdminPermission.SuperAdmin);

                    Log.Info($"[ChildrenAdminComponent] 已为Unit {unit.Id} 添加儿童管理员组件");
                }
            }

            await ETTask.CompletedTask;
        }
    }
}

