namespace ET.Server
{
    public static class UnitLoadHelper
    {
        public static async ETTask<(bool, Unit)> LoadUnit(Player player)
        {
            GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await GateMapFactory.Create(gateMapComponent, player.Id, IdGenerater.Instance.GenerateInstanceId(), "GateMap");
           
            Unit unit = await UnitCacheHelper.GetUnitCache(player.Root(), gateMapComponent.Scene, player.UnitId);
            
            bool isNewUnit = unit == null;
            if (isNewUnit)
            {
                unit = UnitFactory.Create(gateMapComponent.Scene, player.UnitId, UnitType.Player );
                unit.AddComponent<UnitDBSaveComponent>();
            
                UnitCacheHelper.AddOrUpdateUnitAllCache(unit);
            }
            else
            {
                if (unit.GetComponent<UnitDBSaveComponent>() == null)
                {
                    unit.AddComponent<UnitDBSaveComponent>();
                }
            }
            float speed = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);
            if (speed < 0.01f)
            {
                unit.GetComponent<NumericComponent>().Set(NumericType.Speed, 6f); // 设置默认速度
                Log.Error($"Unit speed is {unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed)}");
            }
            
            return (isNewUnit, unit);
        }
    }
}