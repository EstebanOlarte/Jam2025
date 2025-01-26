using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Menus")]

    [Header("Popup Items")]
    [SerializeField] private LosePopup _losePopup;
    [SerializeField] private TopBannerUI _topBannerUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GridManager.Instance.LoseGame += OnLoseGame;
        GameManager.Instance.WaveChange += OnWaveChange;
    }

    private void OnLoseGame()
    {
        Time.timeScale = 0;
        _losePopup.gameObject.SetActive(true);
    }

    private void OnWaveChange (int newWave)
    {
        _topBannerUI.UpdateWave(newWave);
    }


}
