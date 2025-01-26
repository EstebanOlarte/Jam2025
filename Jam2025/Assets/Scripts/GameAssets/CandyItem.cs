using System.Collections;
using UnityEngine;
using Coffee.UIExtensions;

public class CandyItem : MonoBehaviour
{
    [SerializeField] private CandyUI _candyUI;
    private CandySO _candyType;

    [SerializeField] private float _padding = 10;

    private RectTransform _rectTransform;

    private Vector2Int _currentPos;

    private Vector2Int _gridSize;
    private Vector2 _panelGridSize;

    private bool _isBlocked = false;

    public Vector2Int CurrentPos => _currentPos;
    public bool IsBlocked => _isBlocked;

    [Header("Movement")]
    [SerializeField] private AnimationCurve _swapMovementCurve;
    [SerializeField] private float _swapMovementTime;

    [SerializeField] private AnimationCurve _dropMovementCurve;
    [SerializeField] private float _dropMovementTime;

    public CandySO CandyType { get => _candyType; private set => _candyType = value; }
    public float Padding => _padding;

    public void Init(CandySO candySO, Vector2Int gridSize, Vector2Int pos)
    {
        _candyType = candySO;
        _candyUI.SetUpCandy(candySO);

        _gridSize = gridSize;
        _panelGridSize = transform.parent.GetComponent<RectTransform>().sizeDelta;

        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.sizeDelta = new Vector2((_panelGridSize.x / _gridSize.x) - (_padding), (_panelGridSize.y / _gridSize.y) - (_padding));
        MoveTo(pos);

        var candyInput = GetComponent<CandyDirectionHandler>();
        candyInput.DropCandy += OnDropCandy;
    }

    private void OnDestroy()
    {
        var candyInput = GetComponent<CandyDirectionHandler>();
        candyInput.DropCandy -= OnDropCandy;
    }

    public void SetParticles(UIParticleAttractor attractor)
    {
        attractor.AddParticleSystem(_candyUI.UIParticleSystem);
    }

    private void OnDropCandy(Vector2Int dir)
    {
        Vector2Int targetPos = _currentPos + dir;

        if (targetPos.x < 0 || targetPos.x >= _gridSize.x ||
            targetPos.y < 0 || targetPos.y >= _gridSize.y )
        {
            return;
        }
        GetComponent<TriggerAnimationSFX>().Trigger();
        GridManager.Instance.SwapCandy(_currentPos, targetPos);
    }

    public void MoveTo(Vector2Int target, bool animate = false)
    {
        Vector3 targetPos = new Vector3(
            (target.x * (_panelGridSize.x / _gridSize.x)) + (_padding / 2f),
            (target.y * (_panelGridSize.y / _gridSize.y)) + (_padding / 2f), 0);

        _currentPos = target;

        if (animate)
        {
            StartCoroutine(MoveCoroutine(targetPos, _swapMovementTime, _swapMovementCurve));
        }
        else
        {
            _rectTransform.anchoredPosition = targetPos;
        }

    }

    public void Drop(Vector2Int target)
    {
        Vector3 targetPos = new Vector3(
            (target.x * (_panelGridSize.x / _gridSize.x)) + (_padding / 2f),
            (target.y * (_panelGridSize.y / _gridSize.y)) + (_padding / 2f), 0);

        _currentPos = target;

        float time = Vector3.Distance(_rectTransform.anchoredPosition, targetPos) * _dropMovementTime * 0.01f;

        StartCoroutine(MoveCoroutine(targetPos, time, _dropMovementCurve));
    }

    private IEnumerator MoveCoroutine(Vector3 target, float time, AnimationCurve curve)
    {
        Vector3 startPos = _rectTransform.anchoredPosition;
        Vector3 targetPos = target;

        float currentTime = 0;

        while (currentTime <= time)
        {
            float t = curve.Evaluate(currentTime / time);

            var currentPos = Vector3.Lerp(startPos, targetPos, t);

            _rectTransform.anchoredPosition = currentPos;

            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _rectTransform.anchoredPosition = target;
    }

    public void Explode()
    {
        _candyUI.Explode(()=>Destroy(gameObject));
    }

    public void Block()
    {
        _isBlocked = true;
        _candyUI.Block(true);
    }
    public void Unblock()
    {
        _isBlocked = false;
        _candyUI.Block(false);
    }
}
