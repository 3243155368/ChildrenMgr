using System;

namespace ET.Server
{
    [Event(SceneType.All)]
    public class UnitGetComponent_GetComponent:AEvent<Scene,UnitGetComponent>
    {
        protected override async ETTask Run(Scene scene, UnitGetComponent a)
        {
            Unit unit = a.Unit;
            Type type = a.Type;
            unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
            
            //判定Unit身上是否存在需要获取的组件
            if (unit.Components.ContainsKey(type.TypeHandle.Value.ToInt64()))
            {
                return;
            }
            
            UnitDBSaveComponent unitDBSaveComponent = unit.GetComponent<UnitDBSaveComponent>();
            if (unitDBSaveComponent == null)
            {
                return;
            }
            
            //Unit身上不存在需要挂的组件，这个时候就从字节数组容器获取，并进行发序列化挂载到Unit身上
            if (!unit.GetComponent<UnitDBSaveComponent>().Bytes.TryGetValue(type,out byte[] bs))
            {
                return;
            }
            
            //这里的意图就是延迟组件的反序列化的时机，玩家用到对应组件再对需要的组件进行反序列化挂载，抹平CPU消耗尖峰
            Entity entity = MongoHelper.Deserialize(type,bs) as Entity;
            unit.AddComponent(entity);
            await ETTask.CompletedTask;
        }
    }
}