using YIUIFramework;

namespace ET.Client
{
    /// <summary>
    /// Author  YIUI
    /// Date    2025.9.19
    /// Desc
    /// </summary>
    public partial class NewLobbyPanelComponent : Entity, IYIUIOpen<int[]>, IYIUIOpen<int[], ParamVo>
    {
        public EntityRef<YIUILoopScrollChild> m_LoopScroll;
        public YIUILoopScrollChild LoopScroll => m_LoopScroll;
        
        public int[] HasDataGradeClassIds;
        
        public ParamVo ChooseGradeClassIdParamVo;
        
        public bool IsGradeClassSelect;
    }
}
