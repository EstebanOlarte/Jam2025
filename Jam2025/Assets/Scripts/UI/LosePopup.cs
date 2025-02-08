using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : Popup
{
    [SerializeField] private TMP_Text _scoreTMP;
    [SerializeField] private TMP_Text _highScoreTMP;
    [SerializeField] private GameObject _newHighScore;
    [SerializeField] private Transform _container;

    [SerializeField] private GameObject _candieText;
    [SerializeField] private Button _resetBtn;
    [SerializeField] private Button _backMenuBtn;

    private const string cHighScore = "Highscore";
    private const string cScore = "Score";

    Dictionary<CandySO, int> candies = new Dictionary<CandySO, int>();

    private void Start()
    {
        _backMenuBtn.onClick.AddListener(() => BackMenu());
        _resetBtn.onClick.AddListener(() => ResetLevel());
        SetUIData();
    }

    private void SetUIData()
    {
        _scoreTMP.text = PlayerPrefs.GetInt(cScore).ToString("N0");
        _highScoreTMP.text = CheckNewHighScore(PlayerPrefs.GetInt(cHighScore)).ToString("N0");
        StartCoroutine(SetCandies());
    }

    private int CheckNewHighScore(int newScore)
    {
        if (PlayerPrefs.HasKey(cHighScore))
        {
            if (PlayerPrefs.GetInt(cHighScore) > PlayerPrefs.GetInt(cScore))
            {
                _newHighScore.SetActive(false);
                return PlayerPrefs.GetInt(cHighScore);
            }
            else
            {
                _newHighScore.SetActive(true);
                PlayerPrefs.SetInt(cHighScore, PlayerPrefs.GetInt(cScore));
                return PlayerPrefs.GetInt(cScore);
            }
        }
        else
        {
            _newHighScore.SetActive(false);
            PlayerPrefs.SetInt(cHighScore, newScore);
            return PlayerPrefs.GetInt(cHighScore);
        }
    }

    private IEnumerator SetCandies()
    {
        foreach (var candy in GameManager.Instance.ResourcesCollected)
        {
            GameObject newCandyText = Instantiate(_candieText, _container);
            newCandyText.SetActive(true);

            TMP_Text candyTMP = newCandyText.GetComponent<TMP_Text>();
            candyTMP.text = $"<sprite={candy.Key.TextReference}> Total colected: {candy.Value}";

            newCandyText.transform.localScale = Vector3.zero;
            LeanTween.scale(newCandyText, Vector3.one, 0.5f).setEaseOutBack().setIgnoreTimeScale(true);

            yield return null;
        }
    }

    private void BackMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
