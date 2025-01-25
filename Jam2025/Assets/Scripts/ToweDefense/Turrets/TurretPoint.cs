using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPoint : MonoBehaviour
{
    [SerializeField] private Collider _colider;

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
}
