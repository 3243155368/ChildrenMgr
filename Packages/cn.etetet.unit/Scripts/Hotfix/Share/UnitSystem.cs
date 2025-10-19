namespace ET
{
    [EntitySystemOf(typeof(Unit))]
    public static partial class UnitSystem
    {
        [EntitySystem]
        private static void GetComponentSys(this ET.Unit self, System.Type args2)
        {
            if (!typeof(IUnitCache).IsAssignableFrom(args2))
            {
                return;
            }
            EventSystem.Instance.Publish(self.Scene(),new UnitGetComponent(){Unit = self, Type = args2});
        }
        [EntitySystem]
        private static void Awake(this Unit self, int configId)
        {
            self.ConfigId = configId;
        }

        public static UnitConfig Config(this Unit self)
        {
            return UnitConfigCategory.Instance.Get(self.ConfigId);
        }
        
        public static int Type(this Unit self)
        {
            return self.Config().Type;
        }
    }
}