using UnityEngine;

public class CabinAnimatorControl : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float offset = 0f;

    private void Awake()
    {
        animator.SetFloat("AnimatorOffset", offset);
    }
}
