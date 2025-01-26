using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        Vector2 screenResolution = new Vector2(Screen.width, Screen.height);

        Vector2 anchorMin = new Vector2(safeArea.xMin / screenResolution.x, safeArea.yMin / screenResolution.y);
        Vector2 anchorMax = new Vector2(safeArea.xMax / screenResolution.x, safeArea.yMax / screenResolution.y);

        _rectTransform.anchorMin = anchorMin;
        _rectTransform.anchorMax = anchorMax;
        _rectTransform.offsetMin = Vector2.zero;
        _rectTransform.offsetMax = Vector2.zero;
    }
}
