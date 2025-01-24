using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Candy", menuName = "Game asset/Candy")]
public class CandySO : ScriptableObject
{
    public Color Color;

    public CandySO() { }
    public CandySO(CandySO candy)
    {
        Color = candy.Color;
    }

}
