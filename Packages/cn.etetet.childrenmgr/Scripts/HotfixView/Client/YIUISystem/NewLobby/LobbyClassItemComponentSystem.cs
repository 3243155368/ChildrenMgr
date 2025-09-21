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
    [FriendOf(typeof(LobbyClassItemComponent))]
    public static partial class LobbyClassItemComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this LobbyClassItemComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this LobbyClassItemComponent self)
        {
        }

        public static void ResetItem(this LobbyClassItemComponent self, string data)
        {
            self.u_ComClassTxTText.text = data;
        }

        public static void SelectItem(this LobbyClassItemComponent self, bool value)
        {
            self.u_DataU_Select.SetValue(value);
        }

        
        #region YIUIEvent开始
        
        [YIUIInvoke(LobbyClassItemComponent.OnEventSelectInvoke)]
        private static void OnEventSelectInvoke(this LobbyClassItemComponent self)
        {
            self.SelectItem(true);
        }
        #endregion YIUIEvent结束
    }
}
