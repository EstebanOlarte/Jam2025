using UnityEngine;

public class TerrainClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.DeselectTurretPoint();
    }
}
