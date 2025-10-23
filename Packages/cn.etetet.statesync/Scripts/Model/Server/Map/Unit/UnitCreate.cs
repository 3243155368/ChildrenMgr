namespace ET.Server
{
    /// <summary>
    /// Unit创建事件（服务器端）
    /// </summary>
    public struct UnitCreate
    {
        /// <summary>
        /// 创建的Unit
        /// </summary>
        public Unit Unit { get; set; }

        /// <summary>
        /// Unit类型
        /// </summary>
        public int UnitType { get; set; }
    }
}

