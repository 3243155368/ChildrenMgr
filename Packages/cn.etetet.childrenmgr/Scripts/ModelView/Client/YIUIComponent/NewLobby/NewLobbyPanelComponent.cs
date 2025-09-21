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
    public partial class NewLobbyPanelComponent : Entity
    {
        public EntityRef<YIUILoopScrollChild> m_LoopScroll;
        public YIUILoopScrollChild LoopScroll => m_LoopScroll;
        public List<string> LobbyClassData;
    }
}
