using System;
using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //temp
    [SerializeField] private BaseEnemy _basicEnemyPrefab;
    [SerializeField] private BaseEnemy _speedEnemyPrefab;
    [SerializeField] private BaseEnemy _tankEnemyPrefab;

    [SerializeField] private Path _path;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _timeBetweenEnemies = 1f;
    [SerializeField] private int _numberOfEnemies = 10;

    private int _waveNumber = 0;


    private void Start()
    {
        GameManager.Instance.GameStarted += OnGameStarted;
    }

    private void OnDestroy()
    {
        GameManager.Instance.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted(LevelConfigSO sO)
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenWaves);
            _waveNumber++;
            GameManager.Instance.NewWave(_waveNumber);
            yield return SpawnWave();
        }
    }
    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            BaseEnemy enemy = Instantiate(GetEnemyPrefab(), _path.Waypoints[0].position, Quaternion.identity, transform);
            enemy.SetUp(_path.Waypoints, _waveNumber);
            yield return new WaitForSeconds(_timeBetweenEnemies);
        }
    }

    private BaseEnemy GetEnemyPrefab()
    {
        float _basicEnemyWeight = GetBasicEnemyWeight();
        float _speedEnemyWeight = GetSpeedEnemyWeight();
        float _tankEnemyWeight = GetTankEnemyWeight();

        float totalWeight = _basicEnemyWeight + _speedEnemyWeight + _tankEnemyWeight;

        float randomValue = UnityEngine.Random.Range(0, totalWeight);


        if (randomValue <= _basicEnemyWeight)
        {
            return _basicEnemyPrefab;
        }
        else if (randomValue <= _basicEnemyWeight + _speedEnemyWeight)
        {
            return _speedEnemyPrefab;
        }
        else
        {
            return _tankEnemyPrefab;
        }
    }

    private float GetBasicEnemyWeight()
    {
        if (_waveNumber%10 == 3 || _waveNumber%10 == 8)
        {
            return 0.3f;
        }
        else if (_waveNumber % 10 == 5)
        {
            return 1f;
        }
        else if (_waveNumber % 10 == 0)
        {
            return 0.2f;
        }
        else
        {
            return 1f;
        }
    }
    private float GetSpeedEnemyWeight()
    {
        if (_waveNumber % 10 == 3 || _waveNumber % 10 == 8)
        {
            return 1f;
        }
        else if (_waveNumber % 10 == 5)
        {
            return 1f;
        }
        else if (_waveNumber % 10 == 0)
        {
            return 0.4f;
        }
        else
        {
            return 0.1f;
        }
    }
    private float GetTankEnemyWeight()
    {
        if (_waveNumber % 10 == 3 || _waveNumber % 10 == 8)
        {
            return 0.05f;
        }
        else if (_waveNumber % 10 == 5)
        {
            return 0.1f;
        }
        else if (_waveNumber % 10 == 0)
        {
            return 1f;
        }
        else
        {
            return 0.05f;
        }
    }
}
