using UnityEngine;

[CreateAssetMenu(fileName = "Candy", menuName = "Game asset/Candy")]
public class CandySO : ScriptableObject
{
    public string Name;
    public Color Color;
    public Sprite Image;
    [Range(0f,1f)]
    public float Weight;

    public CandySO() { }
    public CandySO(CandySO candy)
    {
        Name = candy.Name;
        Color = candy.Color;
        Weight = candy.Weight;
    }

}
