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

    private void OnLoseGame()
    {
        Time.timeScale = 0;
        LosePopup.gameObject.SetActive(true);
    }

    private void OnWaveChange (int newWave)
    {
        TopBannerUI.UpdateWave(newWave);
    }


}
