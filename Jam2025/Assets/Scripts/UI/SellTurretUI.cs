using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellTurretUI : MonoBehaviour
{
    [SerializeField] private Image _turretImage;
    [SerializeField] private Button _sellButton;
    public void SetUp(TurretSO turretSO)
    {
        _turretImage.sprite = turretSO.Image;
        _sellButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SellTurret();
        });

        gameObject.SetActive(true);
    }
}
