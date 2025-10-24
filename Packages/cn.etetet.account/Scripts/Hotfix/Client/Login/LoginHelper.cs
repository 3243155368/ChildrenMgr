using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask Login(Scene root, string address, string account, string password)
        {
            root.RemoveComponent<ClientSenderComponent>();

            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();

            var response = await clientSenderComponent.LoginAsync(address, account, password);
            if (response.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"请求登录失败，返回错误{response.Error}");
                return;
            }

            Log.Debug("请求登录成功！！！");
            string Token = response.Token;

            //获取服务器列表
            C2R_GetServerInfos c2RGetServerInfos = C2R_GetServerInfos.Create();
            c2RGetServerInfos.Account = account;
            c2RGetServerInfos.Token = response.Token;
            R2C_GetServerInfos r2CGetServerInfos = await clientSenderComponent.Call(c2RGetServerInfos) as R2C_GetServerInfos;
            if (r2CGetServerInfos.Error != ErrorCode.ERR_Success)
            {
                Log.Error("请求服务器列表失败！");
                return;
            }

            ServerInfoProto serverInfoProto = r2CGetServerInfos.ServerInfosList[0];
            Log.Debug($"请求服务器列表成功, 区服名称:{serverInfoProto.ServerName} 区服ID:{serverInfoProto.Id}");

            await EventSystem.Instance.PublishAsync(root, new LoginFinish());

            //获取区服角色列表
            C2R_GetRoles c2RGetRoles = C2R_GetRoles.Create();
            c2RGetRoles.Token = Token;
            c2RGetRoles.Account = account;
            c2RGetRoles.ServerId = serverInfoProto.Id;
            R2C_GetRoles r2CGetRoles = await clientSenderComponent.Call(c2RGetRoles) as R2C_GetRoles;
            if (r2CGetRoles.Error != ErrorCode.ERR_Success)
            {
                Log.Error("请求区服角色列表失败！");
                return;
            }
            EventSystem.Instance.PublishAsync(root,
                new ChooseGradeClassStart { RoleInfo = r2CGetRoles.RoleInfo , Account = account, Token = Token, ServerId = serverInfoProto.Id }).NoContext();
        }

        public static async ETTask CreateRole(Scene root, string account, string token, int serverId, int gradeClassId, List<RoleInfoProto> roleInfos)
        {
            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();
            RoleInfoProto roleInfoProto = default;
            roleInfoProto = roleInfos.FirstOrDefault(info => info.GradeId == gradeClassId);
            //无角色信息 则创建角色信息
            if (roleInfoProto == null)
            {
                C2R_CreateRole c2RCreateRole = C2R_CreateRole.Create();
                c2RCreateRole.Token = token;
                c2RCreateRole.Account = account;
                c2RCreateRole.ServerId = serverId;
                c2RCreateRole.Name = account;
                c2RCreateRole.GradeId = gradeClassId;

                R2C_CreateRole r2CCreateRole = await clientSenderComponent.Call(c2RCreateRole) as R2C_CreateRole;

                if (r2CCreateRole.Error != ErrorCode.ERR_Success)
                {
                    Log.Error("创建区服角色失败！");
                    return;
                }

                roleInfoProto = r2CCreateRole.RoleInfo;
            }

            ClientRoleInfoComponent clientRoleInfoComponent = root.GetComponent<ClientRoleInfoComponent>();
            clientRoleInfoComponent.SetRoleInfoFromProto(roleInfoProto);

            await GetRealmKeyAndEnterMap(root, account, token, serverId, clientSenderComponent, roleInfoProto.Id);
        }

        private static async ETTask GetRealmKeyAndEnterMap(Scene root, string account, string token, int serverId,
        ClientSenderComponent clientSenderComponent, long roleId)
        {
            //请求获取RealmKey
            C2R_GetRealmKey c2RGetRealmKey = C2R_GetRealmKey.Create();
            c2RGetRealmKey.Token = token;
            c2RGetRealmKey.Account = account;
            c2RGetRealmKey.ServerId = serverId;
            R2C_GetRealmKey r2CGetRealmKey = await clientSenderComponent.Call(c2RGetRealmKey) as R2C_GetRealmKey;

            if (r2CGetRealmKey.Error != ErrorCode.ERR_Success)
            {
                Log.Error("获取RealmKey失败！");
                return;
            }

            //请求游戏角色进入Map地图
            NetClient2Main_LoginGame netClient2MainLoginGame =
                    await clientSenderComponent.LoginGameAsync(account, r2CGetRealmKey.Key, roleId, r2CGetRealmKey.Address);
            if (netClient2MainLoginGame.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"进入游戏失败：{netClient2MainLoginGame.Error}");
                return;
            }

            Log.Debug("进入游戏成功！！！");

            root.GetComponent<PlayerComponent>().MyId = netClient2MainLoginGame.PlayerId;
        }
    }
}
