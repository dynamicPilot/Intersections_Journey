using System.Collections;
using UnityEngine;

public class PlaneAnimator : MonoBehaviour
{
    [SerializeField] private float blendParameter = 0;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //animator.SetFloat("Blend", blendParameter);
        animator.enabled = false;
    }

    public void StartAnimator()
    {
        animator.SetFloat("Blend", blendParameter);
        animator.enabled = true;
    }

    public void MakeGetOut()
    {
        animator.SetTrigger("GetOut");
        StartCoroutine(TurnOffGameObject());
    }

    IEnumerator TurnOffGameObject()
    {
        yield return new WaitForSeconds(7f);
        animator.enabled = false;
        gameObject.SetActive(false);

    }

}
