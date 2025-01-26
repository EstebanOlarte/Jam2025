using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelConfigSO _levelConfig;

    public static GameManager Instance { get; private set; }

    private int _score = 0;
    private Dictionary<CandySO, int> _resources = new Dictionary<CandySO, int>();
    private int _priceMultiplier = 1;

    private TurretPoint _selectedTurretPoint;


    public event Action<LevelConfigSO> GameStarted;
    public event Action<Dictionary<CandySO, int>> ResourcesUpdated;
    public event Action DamageTaken;
    public event Action<TurretPoint> TurretPointSelected;
    public event Action<int> WaveChange;
    public event Action<int> ScoreChanged;

    public int PriceMultiplier => _priceMultiplier;
    public int Score => _score;

    private void Awake()
    {
        Instance = this;
    }
    private IEnumerator Start()
    {
        foreach (var candy in _levelConfig.CandyTypes)
        {
            _resources.Add(candy, 0);
        }
        
        yield return new WaitForEndOfFrame();

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
    public bool HasEnoughResources(CandySO candy, int quantity)
    {
        return _resources[candy] >= quantity;
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

    public void AddScore(int score)
    {
        _score += score;
        ScoreChanged?.Invoke(_score);
    }

    //Turrets
    public void SelectTurretPoint(TurretPoint turretPoint)
    {
        _selectedTurretPoint = turretPoint;
        TurretPointSelected?.Invoke(turretPoint);
    }
    public void DeselectTurretPoint()
    {
        _selectedTurretPoint = null;
        TurretPointSelected?.Invoke(null);
    }
    public List<TurretSO> GetTurrets()
    {
        return _levelConfig.Turrets;
    }
    public void BuildTurret(TurretSO turret)
    {
        _selectedTurretPoint.BuildTurret(turret);
        DeselectTurretPoint();

        _priceMultiplier++;
    }

    public void UpgradeTurret(BaseTurret baseTurret)
    {
        baseTurret.Upgrade();
        DeselectTurretPoint();
    }

    public void NewWave(int wave)
    {
        WaveChange?.Invoke(wave);
    }
}
