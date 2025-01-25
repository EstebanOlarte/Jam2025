using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Candy", menuName = "Game asset/Candy")]
public class CandySO : ScriptableObject
{
    public Color Color;
    [Range(0f,1f)]
    public float Rate;

    public CandySO() { }
    public CandySO(CandySO candy)
    {
        Color = candy.Color;
        Rate = candy.Rate;
    }

}
