using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GridManager : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Transform _gridParent;
    [SerializeField] private CandyItem _candyPrefab;

    private List<CandyItem>[] _candyGrid;
    private Vector2Int _gridSize;

    private List<CandySO> _candyTypes;

    private HashSet<CandyItem> _candysToExplode = new HashSet<CandyItem>();
    private List<CandyItem> _candysToCheck = new List<CandyItem>();

    public static GridManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.GameStarted += OnGameStarted;
    }

    private void OnGameStarted(LevelConfigSO levelConfig)
    {
        _candyTypes = levelConfig.CandyTypes;
        _gridSize = levelConfig.GridSize;
        CreateGrid(levelConfig.GridSize);
    }

    private void CreateGrid(Vector2Int gridSize)
    {
        _candyGrid = new List<CandyItem>[gridSize.x];

        for (int i = 0; i < gridSize.x; i++)
        {
            _candyGrid[i] = new List<CandyItem>();
            for (int j = 0; j < gridSize.y * 2; j++)
            {
                var candyOptions = new List<CandySO>(_candyTypes);

                if (j < gridSize.y)
                {
                    if (i > 1)
                    {
                        if (_candyGrid[i - 1][j].CandyType == _candyGrid[i - 2][j].CandyType)
                        {
                            candyOptions.Remove(_candyGrid[i - 1][j].CandyType);
                        }
                    }
                    if (j > 1)
                    {
                        if (_candyGrid[i][j -1].CandyType == _candyGrid[i][j - 2].CandyType)
                        {
                            candyOptions.Remove(_candyGrid[i][j - 1].CandyType);
                        }
                    }
                    if (i > 0 && j > 0)
                    {
                        if ((_candyGrid[i - 1][j].CandyType == _candyGrid[i][j - 1].CandyType) &&
                            (_candyGrid[i - 1][j].CandyType == _candyGrid[i - 1][j - 1].CandyType))
                        {
                            candyOptions.Remove(_candyGrid[i][j - 1].CandyType);
                        }
                    }
                }

                var candyType = GetRandomCandy(candyOptions);

                var candy = Instantiate(_candyPrefab, _gridParent);
                candy.Init(candyType, gridSize, new Vector2Int(i, j));

                _candyGrid[i].Add(candy);
            }
        }

    }

    public void SwapCandy(Vector2Int pos1, Vector2Int pos2)
    {
        StartCoroutine(SwapCandyCoroutine(pos1, pos2));
    }
    private IEnumerator SwapCandyCoroutine(Vector2Int pos1, Vector2Int pos2)
    {
        var candy1 = _candyGrid[pos1.x][pos1.y];
        var candy2 = _candyGrid[pos2.x][pos2.y];
        _candyGrid[pos1.x][pos1.y] = candy2;
        _candyGrid[pos2.x][pos2.y] = candy1;
        candy1.MoveTo(pos2, true);
        candy2.MoveTo(pos1, true);

        yield return new WaitForSeconds(0.5f);

        if (CheckCombinations(new List<Vector2Int> { pos1, pos2 }))
        {
            ExplodeCandys();
        }
        else
        {
            candy1 = _candyGrid[pos1.x][pos1.y];
            candy2 = _candyGrid[pos2.x][pos2.y];
            _candyGrid[pos1.x][pos1.y] = candy2;
            _candyGrid[pos2.x][pos2.y] = candy1;
            candy1.MoveTo(pos2, true);
            candy2.MoveTo(pos1, true);
        }
    }
    private void ExplodeCandys()
    {
        foreach (var item in _candysToExplode)
        {
            RemoveCandy(item);
            item.Explode();
        }

        _candysToExplode.Clear();
        RefillGrid();
    }

    private void RemoveCandy(CandyItem candy)
    {
        _candyGrid[candy.CurrentPos.x].Remove(candy);
    }

    private void RefillGrid()
    {
        for (int i = 0; i < _candyGrid.Length; i++)
        {
            while (_candyGrid[i].Count < _gridSize.y * 2)
            {
                CandySO type = GetRandomCandy(_candyTypes);

                CandyItem candy = Instantiate(_candyPrefab, _gridParent);
                candy.Init(type, _gridSize, new Vector2Int(i, _candyGrid[i].Count));

                _candyGrid[i].Add(candy);
            }
        }
        StartCoroutine(RearangeCandys());
    }

    private IEnumerator RearangeCandys()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _candyGrid.Length; i++)
        {
            for (int j = 0; j < _candyGrid[i].Count; j++)
            {
                if (_candyGrid[i][j].CurrentPos != new Vector2Int(i,j))
                {
                    _candysToCheck.Add(_candyGrid[i][j]);
                    _candyGrid[i][j].Drop(new Vector2Int(i, j));
                }
            }
        }

        yield return new WaitForSeconds(1f);

        if (_candysToCheck.Count > 0)
        {
            List<Vector2Int> _candyspos = new List<Vector2Int>();
            foreach (var item in _candysToCheck)
            {
                _candyspos.Add(item.CurrentPos);
            }

            bool reaction = CheckCombinations(_candyspos);

            _candysToCheck.Clear();
            if (reaction)
            {
                ExplodeCandys();
            }
        }
    }

    private bool CheckCombinations(List<Vector2Int> positions)
    {
        bool result = false;

        foreach (Vector2Int pos in positions)
        {
            bool reaction = CheckHorizontalLines(pos) | CheckVerticalLines(pos) | CheckSquares(pos);

            if (reaction)
            {
                result = true;
            }
        }

        return result;
    }

    private bool CheckHorizontalLines(Vector2Int pos)
    {
        if (pos.y >= _gridSize.y) return false;

        List<CandyItem> conectedCandys = new List<CandyItem>();

        CandyItem candy = _candyGrid[pos.x][pos.y];

        conectedCandys.Add(candy);

        for (int i = pos.x + 1; i < _candyGrid.Length; i++)
        {
            if (_candyGrid[i][pos.y].CandyType == candy.CandyType)
            {
                conectedCandys.Add(_candyGrid[i][pos.y]);
            }
            else
            {
                break;
            }
        }
        for (int i = pos.x - 1; i >= 0; i--)
        {
            if (_candyGrid[i][pos.y].CandyType == candy.CandyType)
            {
                conectedCandys.Add(_candyGrid[i][pos.y]);
            }
            else
            {
                break;
            }
        }

        if (conectedCandys.Count >= 3)
        {
            foreach (var item in conectedCandys)
            {
                _candysToExplode.Add(item);
            }
        }
        return conectedCandys.Count >= 3;
    }

    private bool CheckVerticalLines(Vector2Int pos)
    {
        if (pos.y >= _gridSize.y) return false;

        List<CandyItem> conectedCandys = new List<CandyItem>();

        CandyItem candy = _candyGrid[pos.x][pos.y];

        conectedCandys.Add(candy);


        for (int j = pos.y + 1; j < _gridSize.y; j++)
        {
            if (_candyGrid[pos.x][j].CandyType == candy.CandyType)
            {
                conectedCandys.Add(_candyGrid[pos.x][j]);
            }
            else
            {
                break;
            }
        }
        for (int j = pos.y - 1; j >= 0; j--)
        {
            if (_candyGrid[pos.x][j].CandyType == candy.CandyType)
            {
                conectedCandys.Add(_candyGrid[pos.x][j]);
            }
            else
            {
                break;
            }
        }

        if (conectedCandys.Count >= 3)
        {
            foreach (var item in conectedCandys)
            {
                _candysToExplode.Add(item);
            }
        }
        return conectedCandys.Count >= 3;
    }

    private bool CheckSquares(Vector2Int pos)
    {
        if (pos.y >= _gridSize.y) return false;

        return CheckTopRightSquare(pos) ||
            CheckTopRightSquare(pos + new Vector2Int(0, -1)) ||
            CheckTopRightSquare(pos + new Vector2Int(-1, 0)) ||
            CheckTopRightSquare(pos + new Vector2Int(-1, -1));
    }
    private bool CheckTopRightSquare(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x == _gridSize.x-1 || pos.y == _gridSize.y-1)
        {
            return false;
        }

        if ((_candyGrid[pos.x][pos.y].CandyType == _candyGrid[pos.x + 1][pos.y].CandyType) &&
            (_candyGrid[pos.x][pos.y].CandyType == _candyGrid[pos.x][pos.y + 1].CandyType) &&
            (_candyGrid[pos.x][pos.y].CandyType == _candyGrid[pos.x + 1][pos.y + 1].CandyType))
        {
            _candysToExplode.Add(_candyGrid[pos.x][pos.y]);
            _candysToExplode.Add(_candyGrid[pos.x + 1][pos.y]);
            _candysToExplode.Add(_candyGrid[pos.x][pos.y + 1]);
            _candysToExplode.Add(_candyGrid[pos.x + 1][pos.y + 1]);

            return true;
        }
        else
        {
            return false;
        }
    }

    private CandySO GetRandomCandy(List<CandySO> options)
    {
        CandySO selectedCandy = null;

        Dictionary<CandySO, float> weightedOptions = new Dictionary<CandySO, float>();
        float totalWeight = 0;
        foreach (var item in options)
        {
            weightedOptions.Add(item, item.Rate);
            totalWeight += item.Rate;
        }

        float randomValue = UnityEngine.Random.Range(0, totalWeight);

        if (totalWeight > 0)
        {
            foreach (var item in weightedOptions)
            {
                randomValue -= item.Value;
                if (randomValue <= 0)
                {
                    selectedCandy = item.Key;
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("No candy options");
        }

        return selectedCandy;
    }
}
