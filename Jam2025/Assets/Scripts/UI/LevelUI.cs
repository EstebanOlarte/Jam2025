using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private BaseTurret _turret;

    private void Start()
    {
        _turret.LevelChanged += UpdateLevelText;
        UpdateLevelText(_turret.Level);
    }

    private void OnDestroy()
    {
        _turret.LevelChanged -= UpdateLevelText;
    }

    private void UpdateLevelText(int lvl)
    {
        _levelText.text = $"{lvl}";
    }
}
