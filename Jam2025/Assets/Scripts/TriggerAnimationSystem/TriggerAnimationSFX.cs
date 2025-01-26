using UnityEngine;

public class TriggerAnimationSFX : TriggerAnimation<TriggerAnimationSFXData>
{
    protected override string BasePrefabPath => "Prefabs/TriggerableAnimations/BaseTriggerableAnimations/TriggerableAnimationSFXItem";

    protected override void OnTrigger(Transform target, Vector3 position, TriggerAnimationSFXData data)
    {
        TriggerAnimationSFXItem ob = Resources.Load<TriggerAnimationSFXItem>(this.BasePrefabPath);
        if (ob == null) {
            return;
        }
        TriggerAnimationSFXItem instantiatedOb = Instantiate(ob);
        instantiatedOb.name = $"Instance: {data.path}";
        instantiatedOb.SetData(target, position, data);
        instantiatedOb.Trigger();
    }
}
