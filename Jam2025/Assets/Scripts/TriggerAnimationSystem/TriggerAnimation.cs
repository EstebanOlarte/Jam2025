using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class TriggerAnimation<T> : MonoBehaviour where T : TriggerAnimationData
{
    [SerializeField]
    protected T triggerAnimationData;

    private bool alreadyPlayed = false;

    protected abstract string BasePrefabPath { get; }

    protected abstract void OnTrigger(Transform target, T triggerAnimationData);

    protected void Trigger()
    {
        if (this.triggerAnimationData.playOnce && this.alreadyPlayed) {
            return;
        }

        this.alreadyPlayed = true;

        this.OnTrigger(this.transform, this.triggerAnimationData);
    }

    private void OnEnable()
    {
        if (!this.triggerAnimationData.playOnEnable) {
            return;
        }

        this.Trigger();
    }

    private void OnDisable()
    {
        if (!this.triggerAnimationData.playOnDisable) {
            return;
        }

        this.Trigger();
    }
}
