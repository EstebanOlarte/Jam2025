using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class RayTurret : BaseTurret
{
    [SerializeField] private float _damageCooldown;
    [SerializeField] private float _damagePerTic;
    [SerializeField] private int _maxTargets;

    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private LightningBoltScript _rayPrefab;

    private List<LightningBoltScript> _rays = new List<LightningBoltScript>();

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
                    _rays[i].StartObject.transform.position = _bulletPoint.position;
                    _rays[i].EndObject.transform.position = lowEnemies[i].transform.position;

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
            foreach (LightningBoltScript ray in _rays)
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
