using UnityEngine;
using UnityEngine.UI;

public class TurretsUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Button _closeTurrentMenu;
    [SerializeField] private TurretItemUI _turretItemPrefab;

    private TurretSO _selectedTurret;
    private BaseTurret _selectedTurretInstance;

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
            else
            {
                _selectedTurret = point.TurretSO;
                _selectedTurretInstance = point.Turret;

                SetUpTurretUpgrade();
            }
        }
        else
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_container.GetComponent<RectTransform>());
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_container.GetComponent<RectTransform>());
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
        GameManager.Instance.DeselectTurretPoint();
        _closeTurrentMenu.gameObject.SetActive(false);
    }

    private void SetUpTurretUpgrade()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        var turretItem = Instantiate(_turretItemPrefab, _container);
        turretItem.SetUpUpgrade(_selectedTurret, _selectedTurretInstance);

        _closeTurrentMenu.gameObject.SetActive(true);
    }
}
