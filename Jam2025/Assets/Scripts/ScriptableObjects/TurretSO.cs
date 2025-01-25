using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Game asset/Turret")]
public class TurretSO : ScriptableObject
{
    public List<TurretPrice> Price;
    public BaseTurret Prefab;
    public Sprite Image;
}

[Serializable]
public struct TurretPrice
{
    public CandySO CandyType;
    public int Price;
}
