using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationVFXItem : TriggerAnimationItem<TriggerAnimationVFXData>
{
    private ParticleSystem[] particles;

    protected override bool IsPlaying()
    {
        foreach (ParticleSystem particle in this.particles) {
            if (particle != null && !particle.isPaused && particle.isPlaying && particle.IsAlive()) {
                return true;
            }
        }
        return false;
    }

    protected override void OnTrigger()
    {
        if (particles == null || particles.Length <= 0) {
            return;
        }

        this.particles[0].Play();
    }

    protected override void OnInit()
    {
        if (this.particles != null) {
            return;
        }

        GameObject ob = Resources.Load<GameObject>(this.data.path);
        if (ob == null) {
            return;
        }
        Instantiate(ob, this.transform);

        this.particles = this.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in this.particles) {
            ParticleSystem.MainModule mainParticleSystem = particle.main;
            mainParticleSystem.loop = this.data.loop;
            if (this.data.inheritSize && this.target != null) {
                particle.transform.localScale = this.target.localScale;
            }
        }
    }
}
