using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelConfigSO _levelConfig;

    public event Action<LevelConfigSO> GameStarted;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameStarted?.Invoke(_levelConfig);
    }
}
