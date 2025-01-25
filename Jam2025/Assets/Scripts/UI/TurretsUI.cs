using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private TurretItemUI _turretItemPrefab;

    private void Start()
    {
        GameManager.Instance.TurretPointSelected += OnTurretPointSelected;
    }

    private void OnTurretPointSelected(TurretPoint point)
    {
        if (point != null)
        {
            if (!point.HasTurret())
            {
                SetUpTurretOptions();
            }
        }
        else
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void SetUpTurretOptions()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        foreach (var turret in GameManager.Instance.GetTurrets())
        {
            var turretItem = Instantiate(_turretItemPrefab, _container);
            turretItem.SetUp(turret);
        }
    }
}
