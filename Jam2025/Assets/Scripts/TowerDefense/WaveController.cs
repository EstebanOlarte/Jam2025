using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //temp
    [SerializeField] private BaseEnemy _enemyPrefab;

    [SerializeField] private Path _path;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _timeBetweenEnemies = 1f;
    [SerializeField] private int _numberOfEnemies = 10;


    private void Start()
    {
        GameManager.Instance.GameStarted += OnGameStarted;
    }

    private void OnGameStarted(LevelConfigSO sO)
    {
        StartCoroutine(SpawnWaves());
    }
    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return SpawnWave();
            yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }
    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            BaseEnemy enemy = Instantiate(_enemyPrefab, _path.Waypoints[0].position, Quaternion.identity, transform);
            enemy.SetUp(_path.Waypoints);
            yield return new WaitForSeconds(_timeBetweenEnemies);
        }
    }
}
