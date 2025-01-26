using UnityEngine;
using UnityEngine.UI;

public class CreditsPopup : Popup
{
    [SerializeField] private Button _closeBtn;

    private void Start()
    {
        _closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
