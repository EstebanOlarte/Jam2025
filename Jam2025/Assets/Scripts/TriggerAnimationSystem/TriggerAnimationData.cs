using System;

[Serializable]
public class TriggerAnimationData
{
    public string path = string.Empty;
    public bool loop = false;
    public bool playOnEnable = false;
    public bool playOnDisable = false;
    public bool playOnce = false;
    public bool followTarget = false;

    public float delay = 0;
}
