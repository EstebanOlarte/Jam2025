using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float _health;
    protected float _maxHealth;

    public event Action<float> HealthChanged;

    public float Health => _health;

    protected virtual void Start()
    {
        _maxHealth = _health;
        HealthChanged?.Invoke(1);
    }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
        else
        {
            HealthChanged?.Invoke(_health / _maxHealth);
        }
    }
    public virtual void Heal(float heal)
    {
        _health += heal;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        HealthChanged?.Invoke(_health / _maxHealth);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
