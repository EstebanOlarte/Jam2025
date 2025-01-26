using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _versionTMP;
    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _creditsBtn;
    [SerializeField] private GameObject _creditsPanel;

    private void Start()
    {
        _startGameBtn.onClick.AddListener(LoadScene);
        _versionTMP.text = $"v {Application.version}";
        _creditsBtn.onClick.AddListener(OpenCredits);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    private void OpenCredits()
    {
        _creditsPanel.SetActive(true);
    }
}
