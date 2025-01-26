using System.Collections;
using UnityEngine;

public class OgreSwapController : MonoBehaviour
{
    [SerializeField]
    private GameObject dirty;
    [SerializeField]
    private GameObject clean;
    [SerializeField]
    private float delay;

    private IEnumerator swapCoroutine;

    private void OnEnable()
    {
        if (this.dirty == null || this.clean == null) {
            return;
        }

        this.swapCoroutine = this.LerpIntensityCoroutine();
        this.StartCoroutine(this.swapCoroutine);
    }

    private IEnumerator LerpIntensityCoroutine()
    {
        this.dirty.SetActive(true);
        this.clean.SetActive(false);
        yield return new WaitForSeconds(delay);
        this.dirty.SetActive(false);
        this.clean.SetActive(true);
    }
}
