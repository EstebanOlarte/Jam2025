using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CandyDirectionHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 _startPos;
    private Vector2 _endPos;
    [SerializeField] private float _minDistance = 30;

    public event Action<Vector2Int> DropCandy;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = eventData.position;
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Alguna vibración o escala
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endPos = eventData.position;
        transform.localScale = Vector3.one;
        HandleDragDirection();
    }

    private void HandleDragDirection()
    {
        Vector2 dragVector = _endPos - _startPos;
        if (dragVector.magnitude < _minDistance)
        {
            return;
        }

        float angle = Vector2.SignedAngle(Vector2.right, dragVector);

        if (angle >= -45 && angle <= 45)
        {
            DropCandy?.Invoke(Vector2Int.right);
        }
        else if (angle > 45 && angle < 135)
        {
            DropCandy?.Invoke(Vector2Int.up);
        }
        else if (angle >= 135 || angle <= -135)
        {
            DropCandy?.Invoke(Vector2Int.left);
        }
        else if (angle > -135 && angle < -45)
        {
            DropCandy?.Invoke(Vector2Int.down);
        }
    }
}

