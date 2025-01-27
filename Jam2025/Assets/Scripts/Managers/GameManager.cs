using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelConfigSO _levelConfig;

    public static GameManager Instance { get; private set; }

    private int _score = 0;
    public Dictionary<CandySO, int> Resources = new Dictionary<CandySO, int>();
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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        GameStarted = null;
        ResourcesUpdated = null;
        DamageTaken = null;
        TurretPointSelected = null;
        WaveChange = null;
        ScoreChanged = null;

        Instance = this;
    }

    private IEnumerator Start()
    {
        foreach (var candy in _levelConfig.CandyTypes)
        {
            Resources.Add(candy, 0);
        }
        
        yield return new WaitForEndOfFrame();

        ResourcesUpdated?.Invoke(Resources);
        GameStarted?.Invoke(_levelConfig);
        PlayerPrefs.SetInt("Score", 0);
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
        Resources[candy] += quantity;

        ResourcesUpdated?.Invoke(Resources);
    }
    public bool HasEnoughResources(CandySO candy, int quantity)
    {
        return Resources[candy] >= quantity;
    }
    public bool TryToSpendResource(CandySO candy, int quantity)
    {
        if (Resources[candy] >= quantity)
        {
            Resources[candy] -= quantity;
            ResourcesUpdated?.Invoke(Resources);
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
        PlayerPrefs.SetInt("Score", _score);
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

    public void SellTurret()
    {
        _selectedTurretPoint.SellTurret();
        DeselectTurretPoint();

        _priceMultiplier--;
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
