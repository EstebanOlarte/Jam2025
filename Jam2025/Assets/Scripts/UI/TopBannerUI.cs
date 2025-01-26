using TMPro;
using UnityEngine;

public class TopBannerUI : Menu
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _wave;

    private float elapsedTime = 0f;
    private bool isTimerRunning = false;

    private void Start()
    {
        StartTimer();

        GameManager.Instance.ScoreChanged += UpdateScore;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void UpdateScore(int score)
    {
        _score.text = score.ToString("N0");
    }

    public void UpdateWave(int wave)
    {
        _wave.text = wave.ToString();
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int totalSeconds = Mathf.FloorToInt(elapsedTime);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        // Update the _timer TMP_Text with the appropriate format
        if (hours > 0)
        {
            _timer.text = $"{hours:00}:{minutes:00}:{seconds:00}";
        }
        else
        {
            _timer.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
