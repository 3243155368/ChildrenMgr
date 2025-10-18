namespace ET.Server
{
    [EntitySystemOf(typeof(UnitCache))]
    [FriendOf(typeof(UnitCache))]
    public static partial class UnitCacheSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.UnitCache self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ET.Server.UnitCache self)
        {
            foreach (Entity entityRef in self.CacheComponentDic.Values)
            {
                Entity entity = entityRef;
                entity.Dispose();
            }

            self.CacheComponentDic.Clear();
            self.key = null;
        }

        public static async ETTask<Entity> Get(this ET.Server.UnitCache self, long unitId)
        {
            Entity entity = null;
            if (!self.CacheComponentDic.TryGetValue(unitId, out EntityRef<Entity> entityRef))
            {
                entity = await self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Query<Entity>(unitId, self.key);
                if (entity != null)
                {
                    self.AddOrUpdate(entity);
                }
            }
            else
            {
                entity = entityRef;
            }

            return entity;
        }

        public static void Delete(this ET.Server.UnitCache self, long unitId)
        {
            if (!self.CacheComponentDic.Remove(unitId, out EntityRef<Entity> entityRef))
            {
                return;
            }

            Entity entity = entityRef;
            entity.Dispose();
        }

        public static void AddOrUpdate(this ET.Server.UnitCache self, Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (self.CacheComponentDic.TryGetValue(entity.Id,out EntityRef<Entity> oldEntityRef))
            {
                Entity oldEntity = oldEntityRef;
                if (entity != oldEntity)
                {
                    oldEntity.Dispose();
                }
                self.CacheComponentDic.Remove(entity.Id);
            }

            self.AddChild(entity);
            self.CacheComponentDic.Add(entity.Id, entity);
        }
    }
}