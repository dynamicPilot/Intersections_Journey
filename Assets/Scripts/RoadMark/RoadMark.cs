using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoadMark : MonoBehaviour
{
    [SerializeField] private int roadStartPointNumber = 0;
    public int RoadStartPointNumber { get => roadStartPointNumber; }
    [SerializeField] private RoadMarkSound _sounds;

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

    WaitForSeconds _movingTimer;
    [SerializeField] bool _blockIndicatorUpdate = false;
    [SerializeField] bool _disapearingIsOn = false;
    [SerializeField] bool _needStartAfterEnd = false;

    private void Awake()
    {
        _blockIndicatorUpdate = false;
        _disapearingIsOn = false;
        _needStartAfterEnd = false;
    }

    public bool CheckForStartIndicator()
    {
        _needStartAfterEnd = _disapearingIsOn;
        return !_needStartAfterEnd;
    }

    public void StartIndicator()
    {
        StopAllCoroutines();
        _blockIndicatorUpdate = false;
        _needStartAfterEnd = false;
        _sounds.PlayIndicatorSound();
    }

    public void UpdateIndicatorValue(float newValue, bool isAllert = false)
    {
        if (_blockIndicatorUpdate) return;

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
        }
        
    }

    public void MoveToAllert()
    {
        StopAllCoroutines();
        StartCoroutine(MovingToAllert());
        _sounds.PlayAllertSound();
    }

    IEnumerator MovingToAllert()
    {
        UpdateIndicatorValue(0f, true);
        _blockIndicatorUpdate = true;
        markIndicatorAnimator.SetTrigger("moveToAllert");
        _movingTimer = new WaitForSeconds(moveToAllertDuration);
        yield return _movingTimer;

        markBackgroundAnimator.SetBool("isAllert", true);
        _blockIndicatorUpdate = false;
    }

    public void MoveBackToTimer()
    {
        StopAllCoroutines();
        StartCoroutine(MovingBackToTimer());
        _sounds.PlayIndicatorSound();
    }

    IEnumerator MovingBackToTimer()
    {
        _blockIndicatorUpdate = true;
        _disapearingIsOn = true;
        markBackgroundAnimator.SetBool("isAllert", false);
        markIndicatorAnimator.SetTrigger("backToTimer");
        _movingTimer = new WaitForSeconds(moveBackDuration);
        yield return _movingTimer;

        _blockIndicatorUpdate = false;
        _disapearingIsOn = false;
    }

    public bool StopIndicator()
    {
        if (_disapearingIsOn) return false;

        StopAllCoroutines();

        if (gameObject.activeSelf) StartCoroutine(Disappearing());
        
        return true;
        
    }

    public void ForcedStopIndicator()
    {
        if (!_disapearingIsOn || gameObject.activeSelf)
        {
            StopAllCoroutines();
            _disapearingIsOn = false;
            gameObject.SetActive(false);
        }
    }

    IEnumerator Disappearing()
    {
        _sounds.StopPlaying();
        _disapearingIsOn = true;
        _blockIndicatorUpdate = true;

        markBackgroundAnimator.SetTrigger("makeDisappearance");
        
        _movingTimer = new WaitForSeconds(disappearingDuration);
        yield return _movingTimer;

        if (_needStartAfterEnd)
        {
            markBackgroundAnimator.SetTrigger("makeAppearance");
            _disapearingIsOn = false;
            StartIndicator();
        }
        else
        {
            _disapearingIsOn = false;
            gameObject.SetActive(false);            
        }        
    }
}
