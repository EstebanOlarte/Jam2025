using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTurret : BaseTurret
{
    [SerializeField] private float _damageCooldown;
    [SerializeField] private float _damagePerTic;
    [SerializeField] private int _maxTargets;

    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private LineRenderer _rayPrefab;

    private List<LineRenderer> _rays = new List<LineRenderer>();

    private float _timer = 0;

    private void Update()
    {
        List<BaseEnemy> lowEnemies = GetLowEnemies();

        if (lowEnemies.Count > 0)
        {
            while (_rays.Count < lowEnemies.Count)
            {
                _rays.Add(Instantiate(_rayPrefab, _bulletPoint.position, Quaternion.identity, transform));
            }

            for (int i = 0; i < _rays.Count; i++)
            {
                if (lowEnemies.Count > i)
                {
                    int pointsCount = 7; // 5 intermediate points + start and end points
                    _rays[i].positionCount = pointsCount;

                    Vector3 startPosition = _bulletPoint.position;
                    Vector3 endPosition = lowEnemies[i].transform.position;

                    for (int j = 0; j < pointsCount; j++)
                    {
                        float t = (float)j / (pointsCount - 1);
                        Vector3 intermediatePosition = Vector3.Lerp(startPosition, endPosition, t);
                        _rays[i].SetPosition(j, intermediatePosition);
                    }

                    _rays[i].gameObject.SetActive(true);
                }
                else
                {
                    _rays[i].gameObject.SetActive(false);
                }
            }

            if (_timer <= 0)
            {
                DamageEnemies(lowEnemies);
                _timer = _damageCooldown;
            }
        }
        else
        {
            foreach (LineRenderer ray in _rays)
            {
                ray.gameObject.SetActive(false);
            }
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    private void DamageEnemies(List<BaseEnemy> lowEnemies)
    {
        foreach (BaseEnemy enemy in lowEnemies)
        {
            enemy.TakeDamage(_damagePerTic);
        }
    }

    private List<BaseEnemy> GetLowEnemies()
    {
        List<BaseEnemy> enemiesInRange = new List<BaseEnemy>();

        enemiesInRange = GetEnemiesInRange();
        enemiesInRange.Sort((enemy1, enemy2) => enemy1.Health.CompareTo(enemy2.Health));

        List<BaseEnemy> lowEnemies = new List<BaseEnemy>();

        for (int i = 0; i < _maxTargets; i++)
        {
            if (enemiesInRange.Count > i)
            {
                lowEnemies.Add(enemiesInRange[i]);
            }
            else
            {
                break;
            }
        }

        return lowEnemies;
    }
}
