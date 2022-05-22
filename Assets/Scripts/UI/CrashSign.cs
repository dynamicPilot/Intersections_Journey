using System.Collections;
using TMPro;
using UnityEngine;

public class CrashSign : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator crashEffectAnimator;
    [SerializeField] private TextMeshProUGUI counterText;

    [Header("Settings")]
    [SerializeField] private float counterTextEffectTimer = 0.75f;

    Animator counterAnimator;
    WaitForSeconds timer;

    bool isUpdating = false;
    bool needUpdateAgain = false;

    int newValueForUpdateAgain = -1;

    private void Awake()
    {
        counterAnimator = counterText.GetComponent<Animator>();
        timer = new WaitForSeconds(counterTextEffectTimer);
        isUpdating = false;
        needUpdateAgain = false;
    }

    public void UpdateCounter(int newValue, bool needAnimatorEffect)
    {
        //if (!needAnimatorEffect)
        //{
        //    Logging.Log("CrashSign: no effect");
        //    counterText.SetText(newValue.ToString());
        //    return;
        //}

        Logging.Log("CrashSign: start updating");
        if (isUpdating)
        {
            needUpdateAgain = true;
            newValueForUpdateAgain = newValue;
        }
        else
        {
            StartCoroutine(UpdatingCounter(newValue));
        }
    }

    IEnumerator UpdatingCounter(int newValue)
    {
        // start update
        if (needUpdateAgain)
        {
            needUpdateAgain = false;
            newValueForUpdateAgain = -1;
        }

        isUpdating = true;
        crashEffectAnimator.SetTrigger("OnStartChange");
        counterAnimator.SetTrigger("OnStartChange");
        yield return timer;

        // update value
        counterText.SetText(newValue.ToString());
        counterAnimator.SetTrigger("OnEndChange");
        yield return timer;

        isUpdating = false;

        if (needUpdateAgain)
        {
            StartCoroutine(UpdatingCounter(newValueForUpdateAgain));
        }
    }

}
