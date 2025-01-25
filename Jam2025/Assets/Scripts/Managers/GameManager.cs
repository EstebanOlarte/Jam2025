using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelConfigSO _levelConfig;
    [SerializeField] private List<BaseTurret> turretPrefabs;

    public static GameManager Instance { get; private set; }

    private Dictionary<CandySO, int> _resources = new Dictionary<CandySO, int>();


    public event Action<LevelConfigSO> GameStarted;
    public event Action<Dictionary<CandySO, int>> ResourcesUpdated;
    public event Action DamageTaken;
    public event Action<TurretPoint> TurretPointSelected;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        foreach (var candy in _levelConfig.CandyTypes)
        {
            _resources.Add(candy, 0);
        }
        ResourcesUpdated?.Invoke(_resources);

        GameStarted?.Invoke(_levelConfig);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();
        }
    }

    public void AddResource(CandySO candy, int quantity)
    {
        _resources[candy] += quantity;

        ResourcesUpdated?.Invoke(_resources);
    }
    public bool TryToSpendResource(CandySO candy, int quantity)
    {
        if (_resources[candy] >= quantity)
        {
            _resources[candy] -= quantity;
            ResourcesUpdated?.Invoke(_resources);
            return true;
        }
        return false;
    }
    public void TakeDamage()
    {
        DamageTaken?.Invoke();
    }

    //Turrets
    public void SelectTurretPoint(TurretPoint turretPoint)
    {
        TurretPointSelected?.Invoke(turretPoint);
    }
}
