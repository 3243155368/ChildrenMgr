using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ET.Client
{
    /// <summary>
    /// Author  YIUI
    /// Date    2025.9.19
    /// Desc
    /// </summary>
    [FriendOf(typeof(NewLobbyPanelComponent))]
    public static partial class NewLobbyPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this NewLobbyPanelComponent self)
        {
            self.m_LoopScroll =
                    self.AddChild<YIUILoopScrollChild, LoopScrollRect, Type,string>(self.u_ComLoopScrollVerticalLoopVerticalScrollRect,
                        typeof(LobbyClassItemComponent),"u_EventSelect");
            self.LobbyClassData = new List<string>() { "默认大厅", "亲子大厅", "益智大厅", "数学大厅", "英语大厅", "拼音大厅", "国学大厅", "科学大厅", "艺术大厅", "体育大厅" };
        }

        [EntitySystem]
        private static void Destroy(this NewLobbyPanelComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this NewLobbyPanelComponent self)
        {
            self.LoopScroll.ClearSelect();
            await self.LoopScroll.SetDataRefresh(self.LobbyClassData, 0);
            return true;
        }

        [EntitySystem]
        private static void YIUILoopRenderer(this NewLobbyPanelComponent self, LobbyClassItemComponent item, string data, int index, bool select)
        {
            item.ResetItem(data);
            item.SelectItem(select);
        }

        [EntitySystem]
        private static void YIUILoopOnClick(this NewLobbyPanelComponent self, LobbyClassItemComponent item, int data, int index, bool select)
        {
            Log.Error("点击了" + data);
        }
        
        #region YIUIEvent开始

        [YIUIInvoke(NewLobbyPanelComponent.OnEventEnterInvoke)]
        private static async ETTask OnEventEnterInvoke(this NewLobbyPanelComponent self)
        {
            await ETTask.CompletedTask;
        }

        #endregion YIUIEvent结束
    }
}