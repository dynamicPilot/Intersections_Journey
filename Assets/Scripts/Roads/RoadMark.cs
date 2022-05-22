using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoadMark : MonoBehaviour
{
    [SerializeField] private int roadStartPointNumber = 0;
    public int RoadStartPointNumber { get => roadStartPointNumber; }
    [Header("UI Elements")]
    [SerializeField] private Image indicatorImage;
    [SerializeField] private Image indicatorAllertImage;

    [Header("Animators")]
    [SerializeField] private Animator markBackgroundAnimator;
    [SerializeField] private Animator markIndicatorAnimator;

    [Header("Animator Offsets")]
    [SerializeField] private float moveToAllertDuration = 1.75f;
    [SerializeField] private float moveBackDuration = 1.5f;
    [SerializeField] private float disappearingDuration = 0.5f;

    WaitForSeconds movingTimer;
    //Coroutine appearanceCoroutine;
    bool blockIndicatorUpdate = false;
    bool disapearingIsOn = false;
    bool needStartAfterEnd = false;
    private void Awake()
    {
        blockIndicatorUpdate = false;
        disapearingIsOn = false;
        needStartAfterEnd = false;
    }

    public bool CheckForStartIndicator()
    {
        needStartAfterEnd = disapearingIsOn;
        return !needStartAfterEnd;
    }

    public void StartIndicator()
    {
        StopAllCoroutines();
        blockIndicatorUpdate = false;
        needStartAfterEnd = false;
    }

    public void UpdateIndicatorValue(float newValue, bool isAllert = false)
    {        
        if (blockIndicatorUpdate)
        {
            //Debug.Log("RoadMark: update is blocked");
            return;
        }


        if (newValue > 1) newValue = 1;
        else if (newValue < 0) newValue = 0;

        
        if (isAllert)
        {
            indicatorImage.fillAmount = 1f;
            indicatorAllertImage.fillAmount = newValue;
        }           
        else
        {
            indicatorImage.fillAmount = newValue;
            //Logging.Log("RoasMark: indicator is " + indicatorImage.fillAmount);
        }
        
    }

    public void MoveToAllert()
    {
        StopAllCoroutines();
        StartCoroutine(MovingToAllert());
    }

    IEnumerator MovingToAllert()
    {
        UpdateIndicatorValue(0f, true);
        blockIndicatorUpdate = true;
        markIndicatorAnimator.SetTrigger("moveToAllert");
        movingTimer = new WaitForSeconds(moveToAllertDuration);
        yield return movingTimer;

        markBackgroundAnimator.SetBool("isAllert", true);
        blockIndicatorUpdate = false;
    }
    //backToTimer

    public void MoveBackToTimer()
    {
        StopAllCoroutines();
        StartCoroutine(MovingBackToTimer());
    }

    IEnumerator MovingBackToTimer()
    {
        blockIndicatorUpdate = true;
        disapearingIsOn = true;
        markBackgroundAnimator.SetBool("isAllert", false);
        markIndicatorAnimator.SetTrigger("backToTimer");
        movingTimer = new WaitForSeconds(moveBackDuration);
        yield return movingTimer;

        blockIndicatorUpdate = false;
        disapearingIsOn = false;
    }

    public void StopIndicator()
    {
        if (disapearingIsOn) return;

        StopAllCoroutines();
        if (gameObject.activeSelf) StartCoroutine(Disappearing());
        
    }

    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //}

    IEnumerator Disappearing()
    {
        //Logging.Log("Make Disappearing.....");
        disapearingIsOn = true;
        blockIndicatorUpdate = true;

        markBackgroundAnimator.SetTrigger("makeDisappearance");
        
        movingTimer = new WaitForSeconds(disappearingDuration);
        yield return movingTimer;

        if (needStartAfterEnd)
        {
            markBackgroundAnimator.SetTrigger("makeAppearance");
            disapearingIsOn = false;
            StartIndicator();

        }
        else
        {
            disapearingIsOn = false;
            //Logging.Log("Stop Disappearing.....");
            gameObject.SetActive(false);            
        }
        
    }
}
