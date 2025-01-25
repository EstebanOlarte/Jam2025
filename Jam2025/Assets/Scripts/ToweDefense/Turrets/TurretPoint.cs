using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPoint : MonoBehaviour
{
    [SerializeField] private Collider _colider;
    [SerializeField] private Transform _turretPoint;

    private BaseTurret _turret;

    private void OnMouseDown()
    {
        if (_colider != null)
        {
            GameManager.Instance.SelectTurretPoint(this);
        }
    }

    public bool HasTurret()
    {
        return _turret != null;
    }

    public void BuildTurret(BaseTurret prefab)
    {
        _turret = Instantiate(prefab, _turretPoint.position, Quaternion.identity, transform);
    }
}
