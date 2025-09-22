using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{

    /// <summary>
    /// 由YIUI工具自动创建 请勿修改
    /// </summary>
    [YIUI(EUICodeType.Panel, EPanelLayer.Panel)]
    [ComponentOf(typeof(YIUIChild))]
    public partial class NewLobbyPanelComponent : Entity, IDestroy, IAwake, IYIUIBind, IYIUIInitialize, IYIUIOpen
    {
        public const string PkgName = "NewLobby";
        public const string ResName = "NewLobbyPanel";

        public EntityRef<YIUIChild> u_UIBase;
        public YIUIChild UIBase => u_UIBase;
        public EntityRef<YIUIWindowComponent> u_UIWindow;
        public YIUIWindowComponent UIWindow => u_UIWindow;
        public EntityRef<YIUIPanelComponent> u_UIPanel;
        public YIUIPanelComponent UIPanel => u_UIPanel;
        public UnityEngine.UI.LoopVerticalScrollRect u_ComLoopScrollVerticalLoopVerticalScrollRect;
        public UITaskEventP0 u_EventReturn;
        public UITaskEventHandleP0 u_EventReturnHandle;
        public const string OnEventReturnInvoke = "NewLobbyPanelComponent.OnEventReturnInvoke";
        public UITaskEventP0 u_EventEnter;
        public UITaskEventHandleP0 u_EventEnterHandle;
        public const string OnEventEnterInvoke = "NewLobbyPanelComponent.OnEventEnterInvoke";

    }
}