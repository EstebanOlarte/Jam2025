using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurret : BaseTurret
{
    [SerializeField] private float _bulletCooldown;
    [SerializeField] private float _damage;

    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private TriggerAnimationVFX _animationVFXMuzzleFlash;

    private float _timer = 0;

    private void Update()
    {
        BaseEnemy firstEnemy = GetFirstEnemy();

        if (firstEnemy != null)
        {
            AimToEnemy(firstEnemy);
            if (_timer <= 0)
            {
                StartCoroutine(FireTo(firstEnemy));
                _timer = _timer = _bulletCooldown;
            }
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    private BaseEnemy GetFirstEnemy()
    {
        List<BaseEnemy> enemiesInRange = GetEnemiesInRange();
        if (enemiesInRange == null || enemiesInRange.Count == 0)
        {
            return null;
        }

        BaseEnemy furthestEnemy = null;
        float maxDistance = 0;

        foreach (BaseEnemy enemy in enemiesInRange)
        {
            if (enemy.GetDistanceTraveled() > maxDistance)
            {
                maxDistance = enemy.GetDistanceTraveled();
                furthestEnemy = enemy;
            }
        }

        return furthestEnemy;
    }
    private void AimToEnemy(BaseEnemy enemy)
    {
        if (enemy.transform.position.x > transform.position.x)
        {
            _modelTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            _modelTransform.localScale = new Vector3(-1, 1, 1);
        }

        //Vector3 direction = (enemy.transform.position - transform.position).normalized;
        //direction.y = 0; // Keep only the horizontal direction

        //Quaternion lookRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    private IEnumerator FireTo(BaseEnemy enemy)
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletPoint.position, Quaternion.identity);
        bullet.SetActive(true);
        _animationVFXMuzzleFlash?.Trigger();

        while (enemy != null && Vector3.Distance(bullet.transform.position, enemy.transform.position) > 0.1f)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, enemy.transform.position, Time.deltaTime * _bulletSpeed);
            yield return null;
        }
        Destroy(bullet);
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
        }
    }

    override public void Upgrade()
    {
        base.Upgrade();
        _damage *= 1.2f;
        _bulletCooldown *= 0.9f;
    }

}
