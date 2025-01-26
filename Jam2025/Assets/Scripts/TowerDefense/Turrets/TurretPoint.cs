using System.Collections.Generic;
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

    public void SellTurret()
    {
        if (_turret != null)
        {
            List<TurretPrice> turretPrices = _turretSO.GetTurretPrice();

            float refoundPercentage = 0.6f;

            if (_turret.Level == 1)
            {
                foreach (var item in turretPrices)
                {
                    GameManager.Instance.AddResource(item.CandyType, (int)(item.Price * refoundPercentage));
                }
            }
            else
            {
                foreach (var item in _turretSO.GetTurretUpgradePrice(_turret.Level - 1))
                {
                    float price = item.Price;

                    foreach (var turretPrice in turretPrices)
                    {

                        if (item.CandyType == turretPrice.CandyType)
                        {
                            price += item.Price;
                        }
                    }

                    GameManager.Instance.AddResource(item.CandyType, (int)(price * refoundPercentage));
                }
            }

            Destroy(_turret.gameObject);
            _turret = null;
            _turretSO = null;
        }
    }
}
