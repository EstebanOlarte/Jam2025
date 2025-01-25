using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        GameManager.Instance.ResourcesUpdated += OnResourcesUpdated;
    }

    private void OnResourcesUpdated(Dictionary<CandySO, int> dictionary)
    {
        _text.text = "";

        foreach (var item in dictionary)
        {
            _text.text += $"{item.Key.Name}: {item.Value} | ";
        }

    }
}
