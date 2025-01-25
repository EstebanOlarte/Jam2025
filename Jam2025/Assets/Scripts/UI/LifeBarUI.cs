using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarUI : MonoBehaviour
{
    private Transform mainCam;

    [SerializeField] private Entity _entity;
    [SerializeField] private RectTransform _lifeBar;

    private void Awake()
    {
        mainCam = Camera.main.transform;
        _entity.HealthChanged += UpdateLifeBar;
    }

    private void Update()
    {
        transform.LookAt(transform.position + mainCam.forward);
    }

    private void UpdateLifeBar(float life)
    {
        _lifeBar.localScale = new Vector3(life, 1, 1);
    }
}
