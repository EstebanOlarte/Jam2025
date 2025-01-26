using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Menus")]
    [SerializeField] private List<Menu> _menus;

    [Header("Popup Items")]
    [SerializeField] private List<Popup> _popups;

    private void Awake()
    {
        Instance = this;
    }

}
