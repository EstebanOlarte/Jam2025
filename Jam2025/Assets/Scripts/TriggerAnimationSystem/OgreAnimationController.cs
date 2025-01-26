using UnityEngine;

public class OgreAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void Start() {
        GameManager.Instance.DamageTaken += OnDamageTaken;
        GridManager.Instance.LoseGame += OnDead;
    }

    private void OnDisable() {
        GameManager.Instance.DamageTaken -= OnDamageTaken;
        GridManager.Instance.LoseGame -= OnDead;
    }

    private void OnDamageTaken()
    {
        if (this.animator == null) {
            return;
        }

        this.animator.SetTrigger("Damage");
    }

    private void OnDead()
    {
        this.gameObject.SetActive(false);
    }
}
