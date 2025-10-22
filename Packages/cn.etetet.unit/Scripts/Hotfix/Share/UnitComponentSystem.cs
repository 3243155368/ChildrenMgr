using System.Collections.Generic;

namespace ET
{
	public static partial class UnitComponentSystem
	{
		public static void Add(this UnitComponent self, Unit unit)
		{
		}

		public static Unit Get(this UnitComponent self, long id)
		{
			Unit unit = self.GetChild<Unit>(id);
			return unit;
		}

		public static void Remove(this UnitComponent self, long id)
		{
			Unit unit = self.GetChild<Unit>(id);
			unit?.Dispose();
		}
		public static void GetAll(this UnitComponent self, List<Unit> units)
		{
			foreach (EntityRef<Unit> unitRef in self.Children.Values)
			{
				Unit unit = unitRef;
				units.Add(unit);
			}
		}
	}
}