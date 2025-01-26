using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretItemUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;

    [SerializeField] private Button _button;


    private TurretSO _turret;
    private BaseTurret _baseTurret;

    [SerializeField] private CanvasGroup _canvasGroup;


    public void SetUp(TurretSO turret)
    {
        _turret = turret;

        _image.sprite = turret.Image;
        _name.text = turret.Name;
        _price.text = string.Empty;
        foreach (var price in turret.GetTurretPrice())
        {
            _price.text += $"<sprite={price.CandyType.TextReference}>: {price.Price} \n";
        }

        _button.onClick.AddListener(BuildTurret);

        if (!CheckResources(_turret.GetTurretPrice()))
        {
            _canvasGroup.alpha = 0.1f;
        }

        GameManager.Instance.ResourcesUpdated += OnResourceUpdated;
    }


    public void SetUpUpgrade(TurretSO turret, BaseTurret baseTurret)
    {
        _turret = turret;
        _baseTurret = baseTurret;

        _image.sprite = turret.Image;
        _name.text = turret.Name;
        _price.text = string.Empty;
        foreach (var price in turret.GetTurretUpgradePrice(_baseTurret.Level))
        {
            _price.text += $"<sprite={price.CandyType.TextReference}>: {price.Price} \n";
        }

        _button.onClick.AddListener(UpgradeTurret);

        if (!CheckResources(_turret.GetTurretUpgradePrice(_baseTurret.Level)))
        {
            _canvasGroup.alpha = 0.1f;
        }
        GameManager.Instance.ResourcesUpdated += OnResourceUpdated;
    }
    
    private void OnResourceUpdated(Dictionary<CandySO, int> dictionary)
    {
        if (_baseTurret != null)
        {
            _canvasGroup.alpha = CheckResources(_turret.GetTurretUpgradePrice(_baseTurret.Level)) ? 1f : 0.1f;
        }
        else
        {
            _canvasGroup.alpha = CheckResources(_turret.GetTurretPrice()) ? 1f : 0.1f;
        }
    }

    private void BuildTurret()
    {
        if (CheckResources(_turret.GetTurretPrice()))
        {
            foreach (var item in _turret.GetTurretPrice())
            {
                GameManager.Instance.TryToSpendResource(item.CandyType, item.Price);
            }
            GameManager.Instance.BuildTurret(_turret);
        }
    }
    
    private void UpgradeTurret()
    {
        if (CheckResources(_turret.GetTurretUpgradePrice(_baseTurret.Level)))
        {
            foreach (var item in _turret.GetTurretUpgradePrice(_baseTurret.Level))
            {
                GameManager.Instance.TryToSpendResource(item.CandyType, item.Price);
            }
            GameManager.Instance.UpgradeTurret(_baseTurret);
        }
    }

    private bool CheckResources(List<TurretPrice> turretPrices)
    {
        foreach (var price in turretPrices)
        {
            if (!GameManager.Instance.HasEnoughResources(price.CandyType, price.Price))
            {
                return false;
            }
        }
        return true;
    }

    private void OnDestroy()
    {
        GameManager.Instance.ResourcesUpdated -= OnResourceUpdated;
    }

}
