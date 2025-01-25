using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Config", menuName = "Game asset/Level Config")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] private List<CandySO> _candyTypes;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] public int MinConvinations = 3;

    public List<CandySO> CandyTypes => _candyTypes;
    public Vector2Int GridSize
    {
        get
        {
            Vector2Int grid = _gridSize;

            if (grid.x < 3)
            {
                grid.x = 3;
            }
            if (grid.y < 3)
            {
                grid.y = 3;
            }

            return grid;
        }
        private set { _gridSize = value; }
    }
}
