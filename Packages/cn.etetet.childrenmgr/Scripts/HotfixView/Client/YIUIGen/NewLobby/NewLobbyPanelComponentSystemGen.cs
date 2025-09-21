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
    [FriendOf(typeof(YIUIWindowComponent))]
    [FriendOf(typeof(YIUIPanelComponent))]
    [EntitySystemOf(typeof(NewLobbyPanelComponent))]
    public static partial class NewLobbyPanelComponentSystem
    {
        [EntitySystem]
        private static void Awake(this NewLobbyPanelComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this NewLobbyPanelComponent self)
        {
            self.UIBind();
        }

        private static void UIBind(this NewLobbyPanelComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIChild>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIPanel = self.UIBase.GetComponent<YIUIPanelComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIPanel.Layer = EPanelLayer.Panel;
            self.UIPanel.PanelOption = EPanelOption.TimeCache;
            self.UIPanel.StackOption = EPanelStackOption.VisibleTween;
            self.UIPanel.Priority = 0;
            self.UIPanel.CachePanelTime = 10;

            self.u_ComLoopScrollVerticalLoopVerticalScrollRect = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.LoopVerticalScrollRect>("u_ComLoopScrollVerticalLoopVerticalScrollRect");
            self.u_EventEnter = self.UIBase.EventTable.FindEvent<UITaskEventP0>("u_EventEnter");
            self.u_EventEnterHandle = self.u_EventEnter.Add(self,NewLobbyPanelComponent.OnEventEnterInvoke);

        }
    }
}
