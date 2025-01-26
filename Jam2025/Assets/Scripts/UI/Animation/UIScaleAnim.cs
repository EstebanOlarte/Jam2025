using UnityEngine;

public class UIScaleAnim : MonoBehaviour
{
    [SerializeField] private float minScale = 0.9f; // Minimum scale during the heartbeat
    [SerializeField] private float maxScale = 1.1f; // Maximum scale during the heartbeat
    [SerializeField] private float pulseDuration = 0.5f; // Time for one full scale up or down

    private void Start()
    {
        StartHeartbeatAnimation();
    }

    private void StartHeartbeatAnimation()
    {
        LeanTween.scale(gameObject, Vector3.one * maxScale, pulseDuration)
            .setEaseOutQuad()
            .setLoopPingPong()
            .setOnComplete(() =>
            {
                LeanTween.scale(gameObject, Vector3.one * minScale, pulseDuration).setEaseInQuad();
            });
    }
}
