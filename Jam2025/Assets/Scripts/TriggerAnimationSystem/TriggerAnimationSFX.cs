using UnityEngine;

public class TriggerAnimationSFX : TriggerAnimation<TriggerAnimationSFXData>
{
    protected override string BasePrefabPath => "Prefabs/TriggerableAnimations/BaseTriggerableAnimations/TriggerableAnimationSFXItem";

    protected override void OnTrigger(Transform target, TriggerAnimationSFXData triggerAnimationData)
    {
        TriggerAnimationSFXItem ob = Resources.Load<TriggerAnimationSFXItem>(this.BasePrefabPath);
        if (ob == null) {
            return;
        }
        TriggerAnimationSFXItem instantiatedOb = Instantiate(ob);
        instantiatedOb.name = $"Instance: {triggerAnimationData.path}";
        instantiatedOb.SetData(target, triggerAnimationData);
        instantiatedOb.Trigger();
    }
}
