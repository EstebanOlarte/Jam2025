using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : Entity
{
    private int _level = 1;
    public int Level => _level;

    [SerializeField] private float _range;

    public event Action<int> LevelChanged;

    protected List<BaseEnemy> GetEnemiesInRange()
    {
        List<BaseEnemy> enemiesInRange = new List<BaseEnemy>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range);

        foreach (Collider collider in colliders)
        {
            BaseEnemy enemy = collider.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange;
    }

    private void OnDrawGizmos()
    {
        // Set the color for the gizmos
        Gizmos.color = Color.red;

        // Draw a wire sphere to represent the range of the turret
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    public virtual void Upgrade()
    {
        _level++;
        LevelChanged?.Invoke(_level);
    }
}
