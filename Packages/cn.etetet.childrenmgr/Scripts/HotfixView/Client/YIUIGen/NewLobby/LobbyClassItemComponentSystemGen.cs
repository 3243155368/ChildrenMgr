using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// 由YIUI工具自动创建 请勿修改
    /// </summary>
    [FriendOf(typeof(YIUIChild))]
    [EntitySystemOf(typeof(LobbyClassItemComponent))]
    public static partial class LobbyClassItemComponentSystem
    {
        [EntitySystem]
        private static void Awake(this LobbyClassItemComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this LobbyClassItemComponent self)
        {
            self.UIBind();
        }

        private static void UIBind(this LobbyClassItemComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIChild>();

            self.u_ComClassTxTText = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.Text>("u_ComClassTxTText");
            self.u_DataU_Select = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueBool>("u_DataU_Select");
            self.u_EventSelect = self.UIBase.EventTable.FindEvent<UIEventP0>("u_EventSelect");
            self.u_EventSelectHandle = self.u_EventSelect.Add(self,LobbyClassItemComponent.OnEventSelectInvoke);

        }
    }
}
