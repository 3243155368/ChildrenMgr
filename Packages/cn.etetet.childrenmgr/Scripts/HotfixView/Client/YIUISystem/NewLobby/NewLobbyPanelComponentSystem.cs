using System;
using System.Collections.Generic;
using ET.SchoolGrade;
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
            self.IsGradeClassSelect = true;
        }

        [EntitySystem]
        private static void Destroy(this NewLobbyPanelComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this NewLobbyPanelComponent self)
        {
            await self.SetClassDataRefresh();
            return true;
        }
        private static async ETTask SetClassDataRefresh(this NewLobbyPanelComponent self,SchoolGradeConfig data = null)
        {
            self.LoopScroll.ClearSelect();
            if (!self.IsGradeClassSelect)
            {
                await self.LoopScroll.SetDataRefresh(data.ClassIds_Ref, 0);
            }
            else
            {
                await self.LoopScroll.SetDataRefresh(SchoolGradeConfigCategory.Instance.DataList, 0);
            }
        }

        [EntitySystem]
        private static void YIUILoopRenderer(this NewLobbyPanelComponent self, LobbyClassItemComponent item, SchoolGradeConfig data, int index, bool select)
        {
            item.ResetItem(data);
            item.SelectItem(select);
        }
        [EntitySystem]
        private static void YIUILoopRenderer(this NewLobbyPanelComponent self, LobbyClassItemComponent item, GradeClassConfig data, int index, bool select)
        {
            item.ResetItem(data);
            item.SelectItem(select);
        }

        [EntitySystem]
        private static void YIUILoopOnClick(this NewLobbyPanelComponent self, LobbyClassItemComponent item, SchoolGradeConfig data, int index, bool select)
        {
            item.SelectItem(select);
            if (select)
            {
                self.IsGradeClassSelect = false;
                self.SetClassDataRefresh(data).NoContext(); 
            }
        }
        
        [EntitySystem]
        private static void YIUILoopOnClick(this NewLobbyPanelComponent self, LobbyClassItemComponent item, GradeClassConfig data, int index, bool select)
        {
            item.SelectItem(select);
        }

        #region YIUIEvent开始

        
        [YIUIInvoke(NewLobbyPanelComponent.OnEventReturnInvoke)]
        private static async ETTask OnEventReturnInvoke(this NewLobbyPanelComponent self)
        {
            if (self.IsGradeClassSelect) return;
            self.IsGradeClassSelect = !self.IsGradeClassSelect;
            await self.SetClassDataRefresh();
        }
        
        [YIUIInvoke(NewLobbyPanelComponent.OnEventEnterInvoke)]
        private static async ETTask OnEventEnterInvoke(this NewLobbyPanelComponent self)
        {
            GradeClassConfig gradeClassConfig = self.LoopScroll.GetSelectData<GradeClassConfig>()[0];
            if (gradeClassConfig == null)
            {
                Log.Error("请选择班级");
                return;
            }
            
            await ETTask.CompletedTask;
        }
        #endregion YIUIEvent结束
    }
}