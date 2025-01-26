using UnityEngine;

public class CameraSwipe : MonoBehaviour
{
    private Vector2 _startTouchPosition; // Posición inicial del toque
    private Vector2 _endTouchPosition;   // Posición final del toque
    private bool _isSwiping = false;     // Indica si el usuario está deslizando

    [SerializeField] private float minY = 2.6f; // Límite inferior
    [SerializeField] private float maxY = 4.7f; // Límite superior
    [SerializeField] private float swipeSpeed = 2f; // Velocidad de movimiento de la cámara

    private void Update()
    {
        HandleSwipe();
    }

    private void HandleSwipe()
    {
        if (Input.touchCount > 0) // Comprobar si hay un toque en la pantalla
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Guardar la posición inicial del toque
                    _startTouchPosition = touch.position;
                    _isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (_isSwiping)
                    {
                        // Calcular la diferencia en Y del toque
                        float swipeDeltaY = touch.position.y - _startTouchPosition.y;

                        // Mover la cámara en función del swipe, ajustado por la velocidad
                        float newY = Mathf.Clamp(
                            transform.position.y + (swipeDeltaY * swipeSpeed * Time.deltaTime / Screen.height),
                            minY,
                            maxY
                        );

                        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isSwiping = false; // Terminar el swipe
                    break;
            }
        }
    }
}
