using System.Collections.Generic;
using UnityEngine;

public class TurretPoint : MonoBehaviour
{
    [SerializeField] private Collider _colider;
    [SerializeField] private Transform _turretPoint;
    [SerializeField] private Outline _trunkReference;

    private BaseTurret _turret;
    private TurretSO _turretSO;

    [SerializeField] private Color _withTurret = new Color(1, 0, 0, 1);
    [SerializeField] private Color _withoutTurret = new Color(1, 1, 0, 1);

    public BaseTurret Turret => _turret;
    public TurretSO TurretSO => _turretSO;

    private void OnMouseDown()
    {
        if (_colider != null)
        {
            GameManager.Instance.SelectTurretPoint(this);
        }
    }

    private void Start()
    {
        GameManager.Instance.GameStart += HighLightTurret;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameStart -= HighLightTurret;
    }

    public bool HasTurret()
    {
        return _turret != null;
    }

    public void BuildTurret(TurretSO turretSO)
    {
        _turretSO = turretSO;
        _turret = Instantiate(turretSO.Prefab, _turretPoint.position, Quaternion.identity, transform);
        HighLightTurret();
    }

    public void HighLightTurret()
    {
        if (HasTurret())
        {
            _trunkReference.AnimateOutline(0, 1, _withTurret, 2, 100);
        }
        else
        {
            _trunkReference.AnimateOutline(0, 3, _withoutTurret, 2, 100);
        }

    }

    public void SellTurret()
    {
        if (!HasTurret()) return;

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

        HighLightTurret();
    }
}
