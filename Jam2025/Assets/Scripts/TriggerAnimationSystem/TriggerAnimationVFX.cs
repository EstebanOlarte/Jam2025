using UnityEngine;

public class TriggerAnimationVFX : TriggerAnimation<TriggerAnimationVFXData>
{
    protected override string BasePrefabPath => "Prefabs/TriggerableAnimations/BaseTriggerableAnimations/TriggerableAnimationVFXItem";

    protected override void OnTrigger(Transform target, Vector3 position, Vector3 direction, TriggerAnimationVFXData data)
    {
        TriggerAnimationVFXItem ob = Resources.Load<TriggerAnimationVFXItem>(this.BasePrefabPath);
        if (ob == null) {
            return;
        }
        TriggerAnimationVFXItem instantiatedOb = Instantiate(ob);
        instantiatedOb.name = $"Instance: {data.path}";
        instantiatedOb.SetData(target, position, direction, data);
        instantiatedOb.Trigger();
    }
}
