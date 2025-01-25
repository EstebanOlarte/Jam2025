using UnityEngine;

public abstract class TriggerAnimationItem<T> : MonoBehaviour where T : TriggerAnimationData
{
    protected T data;
    protected Transform target = null;

    private bool hasInitialized = false;

    protected abstract bool IsPlaying();
    protected abstract void OnTrigger();
    protected abstract void OnInit();

    public void SetData(Transform target, T triggerAnimationData)
    {
        this.target = target;
        this.data = triggerAnimationData;
        this.OnInit();
        this.hasInitialized = true;
    }

    public void TriggerDelayed()
    {
        this.OnTrigger();
    }

    protected virtual void Update() {
        if (this.hasInitialized && (!this.IsPlaying() || (this.data.destroyWithTarget && this.target == null))) {
            Object.DestroyImmediate(this.gameObject);
            return;
        }

        if (!this.data.followTarget || this.target == null) {
            return;
        }

        this.transform.position = target.position;
    }

    public void Trigger()
    {
        if (this.data == null) {
            return;
        }

        this.Invoke(nameof(this.TriggerDelayed), this.data.delay);
    }
}
