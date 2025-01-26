using UnityEngine;

public abstract class TriggerAnimationItem<T> : MonoBehaviour where T : TriggerAnimationData
{
    protected T data;
    protected Transform target = null;

    private bool hasInitialized = false;

    protected abstract bool IsPlaying();
    protected abstract void Stop();
    protected abstract void OnTrigger();
    protected abstract void OnInit();

    public void SetData(Transform target, Vector3 position, T triggerAnimationData)
    {
        this.target = target;
        this.data = triggerAnimationData;
        this.transform.position = position;
        this.OnInit();
        this.hasInitialized = true;
    }

    public void TriggerDelayed()
    {
        this.OnTrigger();
    }

    protected virtual void Update() {
        if (!this.hasInitialized) {
            return;
        }

        if (!this.IsPlaying()) {
            Object.DestroyImmediate(this.gameObject);
            return;
        }

        if (this.data.destroyWithTarget && this.target == null) {
            this.Stop();
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
