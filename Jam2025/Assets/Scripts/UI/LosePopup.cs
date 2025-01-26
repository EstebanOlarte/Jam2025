using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : Popup
{
    [SerializeField] private TMP_Text _scoreTMP;
    [SerializeField] private TMP_Text _highScoreTMP;
    [SerializeField] private GameObject _newHighScore;

    [SerializeField] private GameObject _scoreText;
    [SerializeField] private Button _resetBtn;

    private const string cHighScore = "Highscore";

    private void Start()
    {
        _resetBtn.onClick.AddListener(() => ResetLevel());
    }

    public void SetScore(int score)
    {
        _scoreTMP.text = score.ToString();
        CheckNewHighScore(score);
    }

    public void SetHighScore()
    {
        if (PlayerPrefs.HasKey(cHighScore))
        {
            _highScoreTMP.transform.parent.gameObject.SetActive(true);
            _highScoreTMP.text = PlayerPrefs.GetInt(cHighScore).ToString();
        }
        else
        {
            _highScoreTMP.transform.parent.gameObject.SetActive(false);
        }
    }

    private void CheckNewHighScore(int newScore)
    {
        if (!PlayerPrefs.HasKey(cHighScore)) return;

        if (newScore >= PlayerPrefs.GetInt(cHighScore))
        {
            PlayerPrefs.SetInt(cHighScore, newScore);
            _newHighScore.SetActive(true);
        }
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
