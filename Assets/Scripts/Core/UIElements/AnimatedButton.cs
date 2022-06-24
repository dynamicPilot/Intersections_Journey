using System.Collections;
using UnityEngine;

public class AnimatedButton : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Animator Offsets")]
    [SerializeField] private float disappearingDuration = 0.5f;

    WaitForSeconds movingTimer;
    bool disapearingIsOn = false;
    bool needStartAfterEnd = false;

    private void Awake()
    {
        disapearingIsOn = false;
        needStartAfterEnd = false;
    }

    public bool CheckForStartIndicator()
    {
        needStartAfterEnd = disapearingIsOn;
        return !needStartAfterEnd;
    }

    public void StartButton()
    {
        StopAllCoroutines();
        needStartAfterEnd = false;
    }


    public void StopButton()
    {
        if (disapearingIsOn) return;

        StopAllCoroutines();
        if (gameObject.activeSelf) StartCoroutine(Disappearing());

    }

    IEnumerator Disappearing()
    {
        disapearingIsOn = true;

        animator.SetTrigger("makeDisappearance");

        movingTimer = new WaitForSeconds(disappearingDuration);
        yield return movingTimer;

        if (needStartAfterEnd)
        {
            animator.SetTrigger("makeAppearance");
            disapearingIsOn = false;
            StartButton();

        }
        else
        {
            disapearingIsOn = false;
            gameObject.SetActive(false);
        }

    }
}
