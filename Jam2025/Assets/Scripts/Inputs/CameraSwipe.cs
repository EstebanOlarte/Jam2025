using UnityEngine;
using UnityEngine.EventSystems; // Required to check for UI interactions

public class CameraSwipe : MonoBehaviour
{
    private Vector2 _startTouchPosition;
    private bool _isSwiping = false;

    [SerializeField] private float minY = 2.6f;
    [SerializeField] private float maxY = 6.0f;
    [SerializeField] private float swipeSpeed = 5f;
    [SerializeField] private LayerMask _layerIgnore;

    private void Update()
    {
        HandleSwipe();
    }

    private void HandleSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is over a UI element
            if (IsTouchOverUI(touch, _layerIgnore))
            {
                return; // If the touch is on the UI, don't process the swipe
            }

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPosition = touch.position;
                    _isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (_isSwiping)
                    {
                        float swipeDeltaY = touch.position.y - _startTouchPosition.y;

                        // Invert swipe direction
                        swipeDeltaY = -swipeDeltaY;

                        // Calculate the new Y position of the camera
                        float newY = Mathf.Clamp(
                            transform.position.y + (swipeDeltaY * swipeSpeed * Time.deltaTime / Screen.height),
                            minY,
                            maxY
                        );

                        // Update the camera's position
                        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

                        // Update the start position for smoother movement
                        _startTouchPosition = touch.position;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isSwiping = false;
                    break;
            }
        }
    }

    private bool IsTouchOverUI(Touch touch, LayerMask ignoreLayer)
    {
        if (EventSystem.current == null) return false;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = touch.position
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if ((ignoreLayer.value & (1 << result.gameObject.layer)) == 0)
            {
                return true;
            }
        }

        return false;
    }

}
