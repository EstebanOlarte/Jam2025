using Coffee.UIExtensions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesUI : Menu
{
    [SerializeField] private TextMeshProUGUI _text;
    public UIParticleAttractor UIAttractor;

    void Start()
    {
        GameManager.Instance.ResourcesUpdated += OnResourcesUpdated;
    }

    private void OnDestroy()
    {
        GameManager.Instance.ResourcesUpdated -= OnResourcesUpdated;
    }

    private void OnResourcesUpdated(Dictionary<CandySO, int> dictionary)
    {
        _text.text = "";
        int currentResource = 0;
        int resourceSize = dictionary.Count;

        foreach (var item in dictionary)
        {
            currentResource++;
            if (currentResource == dictionary.Count)
            {
                _text.text += $"<sprite={item.Key.TextReference}>: {item.Value}";
            }
            else
            {
                _text.text += $"<sprite={item.Key.TextReference}>: {item.Value} | ";
            }
        }

    }
}
