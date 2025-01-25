using UnityEngine;

public class TriggerAnimationSFXItem : TriggerAnimationItem<TriggerAnimationSFXData>
{
    private AudioSource audioSource;
    protected override bool IsPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }

    protected override void OnInit()
    {
        AudioClip audioClip = Resources.Load<AudioClip>(this.data.path);
        if (audioClip == null) {
            return;
        }

        this.audioSource = this.GetComponent<AudioSource>();

        if (this.audioSource == null) {
            return;
        }

        this.audioSource.clip = audioClip;
        this.audioSource.loop = this.data.loop;
    }

    protected override void OnTrigger()
    {
        if (this.audioSource == null) {
            return;
        }

        this.audioSource.Play();
    }

    protected override void Update()
    {
        base.Update();
        if (this.audioSource == null) {
            return;
        }

        this.audioSource.volume = this.data.volume.Evaluate(this.audioSource.time / this.audioSource.clip.length);
    }
}
