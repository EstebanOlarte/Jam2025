using UnityEngine;
using UnityEngine.UI;

public class TurretsUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Button _closeTurrentMenu;
    [SerializeField] private TurretItemUI _turretItemPrefab;

    private void Start()
    {
        GameManager.Instance.TurretPointSelected += OnTurretPointSelected;
        _closeTurrentMenu.onClick.AddListener(closeMenu);
    }

    private void OnTurretPointSelected(TurretPoint point)
    {
        if (point != null)
        {
            if (!point.HasTurret())
            {
                SetUpTurretOptions();
            }
        }
        else
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void SetUpTurretOptions()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        foreach (var turret in GameManager.Instance.GetTurrets())
        {
            var turretItem = Instantiate(_turretItemPrefab, _container);
            turretItem.SetUp(turret);
        }

        _closeTurrentMenu.gameObject.SetActive(true);
    }

    private void closeMenu()
    {
        GameManager.Instance.SelectTurretPoint(null);
        _closeTurrentMenu.gameObject.SetActive(false);
    }
}
