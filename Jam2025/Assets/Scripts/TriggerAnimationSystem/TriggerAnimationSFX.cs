using UnityEngine;

public class TriggerAnimationSFX : TriggerAnimation<TriggerAnimationSFXData>
{
    protected override string BasePrefabPath => "Prefabs/TriggerableAnimations/BaseTriggerableAnimations/TriggerableAnimationSFXItem";

    protected override void OnTrigger(Transform target, Vector3 position, Vector3 direction, TriggerAnimationSFXData data)
    {
        TriggerAnimationSFXItem ob = Resources.Load<TriggerAnimationSFXItem>(this.BasePrefabPath);
        if (ob == null) {
            return;
        }
        TriggerAnimationSFXItem instantiatedOb = Instantiate(ob);
        instantiatedOb.name = $"Instance: {data.path}";
        instantiatedOb.SetData(target, position, direction, data);
        instantiatedOb.Trigger();
    }

    public void TriggerAudio(Transform target, Vector3 position, Vector3 direction, TriggerAnimationSFXData data)
    {
        OnTrigger(target, position, direction, data);
    }

}
