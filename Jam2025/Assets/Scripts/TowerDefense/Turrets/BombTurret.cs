using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTurret : BaseTurret
{
    [SerializeField] private float _bulletCooldow;
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletRadius;

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Animator _animator;

    private float _timer = 0;

    private void Update()
    {

        if (_timer <= 0)
        {
            List<BaseEnemy> randomEnemies = GetRandomEnemy();
            if (randomEnemies != null)
            {
                StartCoroutine(FireTo(randomEnemies));
                _timer = _bulletCooldow;
            }
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    private List<BaseEnemy> GetRandomEnemy()
    {
        List<BaseEnemy> enemiesInRange = GetEnemiesInRange();
        if (enemiesInRange == null || enemiesInRange.Count == 0)
        {
            return null;
        }

        List<BaseEnemy> randomEnemies = new List<BaseEnemy>();
        int randomIndex = Random.Range(0, enemiesInRange.Count);
        randomEnemies.Add(enemiesInRange[randomIndex]);

        Collider[] colliders = Physics.OverlapSphere(randomEnemies[0].transform.position, _bulletRadius * 1.2f);

        foreach (Collider collider in colliders)
        {
            BaseEnemy enemy = collider.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                if (!randomEnemies.Contains(enemy))
                {
                    randomEnemies.Add(enemy);
                }
            }
        }
        return randomEnemies;
    }

    private IEnumerator FireTo(List<BaseEnemy> enemies)
    {
        if (enemies == null || enemies.Count == 0)
        {
            yield break;
        }

        _animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.35f);

        BaseEnemy targetEnemy = enemies[0];
        Vector3 spawnPosition = targetEnemy.transform.position + Vector3.up * 2f; // Spawn a bit above the enemy
        GameObject bullet = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity, transform);
        bullet.transform.localScale = Vector3.one * _bulletRadius * 2f;
        bullet.SetActive(true);

        while (targetEnemy != null && bullet.transform.position.y > targetEnemy.transform.position.y)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetEnemy.transform.position, Time.deltaTime * 10f);
            yield return null;
        }

        // Apply damage to all enemies in the list
        foreach (BaseEnemy enemy in enemies)
        {
            if (enemy!= null)
            {
                enemy.TakeDamage(_damage);
            }
        }

        // Destroy the bullet after it hits the target
        Destroy(bullet);
    }
}
