using UnityEngine;

public class TurretPoint : MonoBehaviour
{
    [SerializeField] private Collider _colider;
    [SerializeField] private Transform _turretPoint;

    private BaseTurret _turret;
    private TurretSO _turretSO;

    public BaseTurret Turret => _turret;
    public TurretSO TurretSO => _turretSO;

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

    public void BuildTurret(TurretSO turretSO)
    {
        _turretSO = turretSO;
        _turret = Instantiate(turretSO.Prefab, _turretPoint.position, Quaternion.identity, transform);
    }
}
