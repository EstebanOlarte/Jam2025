using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurretItemUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;

    [SerializeField] private Button _button;

    private TurretSO _turret;

    public void SetUp(TurretSO turret)
    {
        _turret = turret;

        _image.sprite = turret.Image;
        _name.text = turret.Name;
        _price.text = string.Empty;
        foreach (var price in turret.GetTurretPrice())
        {
            _price.text += $"{price.CandyType.Name}: {price.Price} \n";
        }

        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (CheckResources())
        {
            foreach (var item in _turret.GetTurretPrice())
            {
                GameManager.Instance.TryToSpendResource(item.CandyType, item.Price);
            }
            GameManager.Instance.BuildTurret(_turret);
        }
    }

    private bool CheckResources()
    {
        foreach (var price in _turret.GetTurretPrice())
        {
            if (!GameManager.Instance.HasEnoughResources(price.CandyType, price.Price))
            {
                Debug.Log("Not enough resources");
                return false;
            }
        }
        return true;
    }
}
