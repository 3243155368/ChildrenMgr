using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  YIUI
    /// Date    2025.9.19
    /// Desc
    /// </summary>
    [FriendOf(typeof(NewLoginPanelComponent))]
    public static partial class NewLoginPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this NewLoginPanelComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this NewLoginPanelComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this NewLoginPanelComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }

        #region YIUIEvent开始
        
        [YIUIInvoke(NewLoginPanelComponent.OnEventLoginInvoke)]
        private static async ETTask OnEventLoginInvoke(this NewLoginPanelComponent self)
        {
            Log.Info($"登录");
            GlobalComponent globalComponent = self.Root().GetComponent<GlobalComponent>();
            await LoginHelper.Login(self.Root(),
                globalComponent.GlobalConfig.Address,
                self.u_ComAccountInputField.text,
                self.u_ComPasswordInputField.text);
        }
        #endregion YIUIEvent结束
    }
}
