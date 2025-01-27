using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Menus")]
    public TopBannerUI TopBannerUI;

    [Header("Popup Items")]
    public LosePopup LosePopup;
    public FeedbackUI FeedbackUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        GridManager.Instance.LoseGame += OnLoseGame;
        GameManager.Instance.WaveChange += OnWaveChange;
    }

    private void OnDestroy()
    {
        GridManager.Instance.LoseGame -= OnLoseGame;
        GameManager.Instance.WaveChange -= OnWaveChange;
    }

    private IEnumerator LoseGameCoroutine()
    {
        float waitTime = 5f;
        float slowDownDuration = 3f; // Duration to slow down time (in seconds)
        float targetTimeScale = 0.1f; // Minimum timescale before stopping

        yield return new WaitForSecondsRealtime(waitTime - slowDownDuration);

        float elapsedTime = 0f;
        float initialTimeScale = Time.timeScale;

        while (elapsedTime < slowDownDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, elapsedTime / slowDownDuration);
            yield return null;
        }

        Time.timeScale = 0f;
        LosePopup.gameObject.SetActive(true);
    }

    private void OnLoseGame()
    {
        StartCoroutine(LoseGameCoroutine());
    }

    private void OnWaveChange (int newWave)
    {
        TopBannerUI.UpdateWave(newWave);
    }


}
