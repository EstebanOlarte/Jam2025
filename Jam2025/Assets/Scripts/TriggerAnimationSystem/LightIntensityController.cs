using System.Collections;
using UnityEngine;

public class LightIntensityController : MonoBehaviour
{
    [SerializeField]
    private Light mLight;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float delay;
    [SerializeField]
    private float from;
    [SerializeField]
    private float to;

    private float time;

    private IEnumerator lerpIntensityCoroutine;

    private void OnEnable()
    {
        if (this.mLight == null) {
            return;
        }

        this.lerpIntensityCoroutine = this.LerpIntensityCoroutine();
        this.StartCoroutine(this.lerpIntensityCoroutine);
    }

    private IEnumerator LerpIntensityCoroutine()
    {
        yield return new WaitForSeconds(delay);
        while (this.time <= this.duration) {
            this.mLight.intensity = Mathf.Lerp(this.from, this.to, this.time / this.duration);
            this.time += Time.deltaTime;
            yield return null;
        }
        this.mLight.intensity = this.to;
    }
}
