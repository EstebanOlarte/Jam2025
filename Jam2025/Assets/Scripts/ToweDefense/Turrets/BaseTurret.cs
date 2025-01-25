using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    [SerializeField] private Dictionary<CandySO, float> _price;
    [SerializeField] private float _health;


    public Dictionary<CandySO, float> Price => _price;
}
