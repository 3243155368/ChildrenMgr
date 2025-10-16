using System;
using System.Collections.Generic;

namespace ET.Server
{
    [MessageHandler(SceneType.UnitCache)]
    [FriendOfAttribute(typeof(ET.Server.UnitCacheComponent))]
    public class Other2UnitCache_GetUnitHandler : MessageHandler<Scene, Other2UnitCache_GetUnit, UnitCache2Other_GetUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_GetUnit request, UnitCache2Other_GetUnit response)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            Dictionary<string, Entity> dict = ObjectPool.Fetch(typeof(Dictionary<string, Entity>)) as Dictionary<string, Entity>;
            try
            {
                if (request.ComponentList.Count == 0)
                {
                    dict.Add("ET.Unit", null);
                    foreach (string s in unitCacheComponent.UnitCacheKeyList)
                    {
                        if (s == "ET.Unit")
                        {
                             continue;
                        }
                        dict.Add(s, null);
                    }
                }
                else
                {
                    foreach (string s in request.ComponentList)
                    {
                        dict.Add(s, null);
                    }
                }
                
                long unitId = request.UnitId;
                
                CoroutineLockComponent coroutineLockComponent = scene.GetComponent<CoroutineLockComponent>();

                using (await coroutineLockComponent.Wait(CoroutineLockType.UnitCacheGet, unitId))
                {
                    unitCacheComponent.CallCache(unitId);

                    using (ListComponent<string> keyList = ListComponent<string>.Create())
                    {
                        foreach (string key in dict.Keys)
                        {
                            keyList.Add(key);
                        }

                        foreach (string key in keyList)
                        {
                            Entity entity = await unitCacheComponent.Get(request.UnitId, key);
                            dict[key] = entity;
                        }
                    }

                    foreach (KeyValuePair<string, Entity> info in dict)
                    {
                        response.ComponentNameList.Add(info.Key);
                        
                        response.EntityList.Add(info.Value?.ToBson() ?? null);
                    }
                }
            }
            finally
            {
               dict.Clear();
               ObjectPool.Recycle(dict);
            }
        }
    }
}