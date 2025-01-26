using UnityEditor.Purchasing;
using UnityEngine;

public abstract class TriggerAnimation<T> : MonoBehaviour where T : TriggerAnimationData
{
    [SerializeField]
    protected T data;

    private bool alreadyPlayed = false;

    protected abstract string BasePrefabPath { get; }

    protected abstract void OnTrigger(Transform target, Vector3 position, Vector3 direction, T triggerAnimationData);

    public void Trigger(bool ignoreAlreadyPlayed = true)
    {
        if (!ignoreAlreadyPlayed && this.data.playOnce && this.alreadyPlayed) {
            return;
        }

        this.alreadyPlayed = true;

        this.OnTrigger(this.transform, this.transform.position, this.transform.forward, this.data);
    }

    private void OnEnable()
    {
        if (!this.data.playOnEnable) {
            return;
        }

        this.Trigger(false);
    }

    private void OnDisable()
    {
        if (!this.data.playOnDisable) {
            return;
        }

        this.Trigger(false);
    }

    public void SetData(T data)
    {
        this.data = data;
    }
}
