using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Game asset/Turret")]
public class TurretSO : ScriptableObject
{
    public string Name;
    public List<TurretPrice> Price;
    public BaseTurret Prefab;
    public Sprite Image;

    public List<TurretPrice> GetTurretPrice()
    {
        List<TurretPrice> price = new List<TurretPrice>();
        foreach (var item in Price)
        {
            TurretPrice turretPrice = new TurretPrice();
            turretPrice.CandyType = item.CandyType;
            turretPrice.Price = item.Price * GameManager.Instance.PriceMultiplier;
            price.Add(turretPrice);
        }

        return price;
    }
}

[Serializable]
public struct TurretPrice
{
    public CandySO CandyType;
    public int Price;
}
