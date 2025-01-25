using UnityEngine;

public abstract class TriggerAnimationItem<T> : MonoBehaviour where T : TriggerAnimationData
{
    protected T triggerAnimationData;
    private Transform target = null;

    private bool hasInitialized = false;

    protected abstract bool IsPlaying();
    protected abstract void OnTrigger();
    protected abstract void OnInit();

    public void SetData(Transform target, T triggerAnimationData)
    {
        this.target = target;
        this.triggerAnimationData = triggerAnimationData;
        this.OnInit();
        this.hasInitialized = true;
    }

    public void TriggerDelayed()
    {
        this.OnTrigger();
    }

    protected virtual void Update() {
        if (this.hasInitialized && !this.IsPlaying()) {
            Object.DestroyImmediate(this.gameObject);
            return;
        }

        if (!this.triggerAnimationData.followTarget || this.target == null) {
            return;
        }

        this.transform.position = target.position;
    }

    public void Trigger()
    {
        if (this.triggerAnimationData == null) {
            return;
        }

        this.Invoke(nameof(this.TriggerDelayed), this.triggerAnimationData.delay);
    }
}
