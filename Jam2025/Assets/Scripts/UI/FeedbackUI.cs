using TMPro;
using UnityEngine;

public class FeedbackUI : Popup
{
    [Header("TowerDefense Feedback")]
    [SerializeField] private Transform _towerDefenseContainer;

    [Header("Match Feedback")]
    [SerializeField] private Transform _match3Container;

    [SerializeField] private GameObject _comboTMPPrefab; // Prefab for combos
    [SerializeField] private float _comboTime = 2f;      // Duration for combo display

    [SerializeField] private TMP_Text _waveWarning;
    [SerializeField] private float _waveWarningTime = 2f; // Duration for wave warning display

    private GameObject _currentCombo;

    /// <summary>
    /// Adds a new combo. Hides the previous combo, resets its timer, and displays the new one with animation.
    /// </summary>
    /// <param name="comboCounter">The combo count to display.</param>
    public void AddCombo(int comboCounter)
    {
        if (_currentCombo != null)
        {
            LeanTween.cancel(_currentCombo);
            Destroy(_currentCombo);
        }

        _currentCombo = Instantiate(_comboTMPPrefab, _match3Container);
        _currentCombo.gameObject.SetActive(true);
        _currentCombo.transform.localPosition = Vector3.zero;
        _currentCombo.transform.localScale = Vector3.one * 1.5f;
        _currentCombo.transform.rotation = Quaternion.Euler(0, 0, 45);

        TMP_Text comboText = _currentCombo.GetComponent<TMP_Text>();
        comboText.text = $"Combo\nX{comboCounter}";

        LeanTween.scale(_currentCombo, Vector3.one, 0.3f).setEaseOutBack();
        LeanTween.rotate(_currentCombo, Vector3.zero, 0.3f).setEaseOutBack();
        LeanTween.alphaCanvas(_currentCombo.GetComponent<CanvasGroup>(), 1f, 0.3f);

        LeanTween.alphaCanvas(_currentCombo.GetComponent<CanvasGroup>(), 0f, _comboTime)
            .setDelay(0.5f)
            .setOnComplete(() => Destroy(_currentCombo));
    }

    /// <summary>
    /// Displays the wave warning for a set time and animates it in and out.
    /// </summary>
    /// <param name="wave">The wave number to display.</param>
    public void WarningWave(int wave)
    {
        _waveWarning.gameObject.SetActive(true);
        // Set the text for the warning
        _waveWarning.text = $"New Wave Incoming\nWave {wave}";

        // Animate: Scale in and fade in
        _waveWarning.transform.localScale = Vector3.zero;
        LeanTween.scale(_waveWarning.gameObject, Vector3.one, 0.3f).setEaseOutBack();
        LeanTween.alphaText(_waveWarning.rectTransform, 1f, 0.3f);

        // Animate: Fade out and scale out after waveWarningTime
        LeanTween.delayedCall(_waveWarningTime, () =>
        {
            LeanTween.scale(_waveWarning.gameObject, Vector3.zero, 0.3f).setEaseInBack();
            LeanTween.alphaText(_waveWarning.rectTransform, 0f, 0.3f).setOnComplete(() =>
                _waveWarning.gameObject.SetActive(false)
            );
        });
    }
}