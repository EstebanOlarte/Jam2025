using UnityEngine;

public class TriggerAnimationVFX : TriggerAnimation<TriggerAnimationVFXData>
{
    protected override string BasePrefabPath => "Prefabs/TriggerableAnimations/BaseTriggerableAnimations/TriggerableAnimationVFXItem";

    protected override void OnTrigger(Transform target, TriggerAnimationVFXData triggerAnimationData)
    {
        TriggerAnimationVFXItem ob = Resources.Load<TriggerAnimationVFXItem>(this.BasePrefabPath);
        if (ob == null) {
            return;
        }
        TriggerAnimationVFXItem instantiatedOb = Instantiate(ob);
        instantiatedOb.name = $"Instance: {triggerAnimationData.path}";
        instantiatedOb.SetData(target, triggerAnimationData);
        instantiatedOb.Trigger();
    }
}
