using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView: AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate args)
        {
            //暂时不用加载UnityView模型
            // Unit unit = args.Unit;
            // // Unit View层
            // string assetsName = $"Packages/cn.etetet.demores/Bundles/Unit/Unit.prefab";
            // GameObject bundleGameObject = await scene.GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            // GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");
            //
            // GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();
            // GameObject go = UnityEngine.Object.Instantiate(prefab, globalComponent.Unit, true);
            // unit.AddComponent<GameObjectComponent>().GameObject = go;
            // unit.AddComponent<AnimatorComponent>();
            await ETTask.CompletedTask;
        }
    }
}