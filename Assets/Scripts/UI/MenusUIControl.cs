using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenusUIControl : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PanelsUI panelsUI;
    [SerializeField] private PauseMenuUI pauseUI;

    [Header("Settings")]
    [SerializeField] private float indicatorUpdateLockdownTimer = 0.92f;

    [Header("----- UI -----")]
    [SerializeField] private CrashSign crashSign;
    [SerializeField] private Image timeIndicatorImage;
    [SerializeField] private Animator indicatorAnimator;

    private int endHour = 0;
    bool isIndicatorInLockdown = false;
    private void Awake()
    {
        SetUI();
    }

    void SetUI()
    {
        UpdateCrashCounter(0);
        StartCoroutine(IndicatorUpdateLockdown());
    }

    IEnumerator IndicatorUpdateLockdown()
    {
        isIndicatorInLockdown = true;
        yield return new WaitForSeconds(indicatorUpdateLockdownTimer);
        indicatorAnimator.enabled = false;
        isIndicatorInLockdown = false;
    }

    public void OpenStartMenu(int levelNumber, int maxCrashesNumber)
    {
        panelsUI.SetStartPanel(levelNumber, maxCrashesNumber);
    }

    public void OpenEndLevelMenu(int levelNumber)
    {
        panelsUI.SetEndLevelOrGameOverPanel(1, levelNumber, true);
    }

    public void OpenGameOverMenu(int levelNumber)
    {
        panelsUI.SetEndLevelOrGameOverPanel(0, levelNumber, true);
    }

    public void OpenPauseMenu()
    {
        pauseUI.StartPause();
    }

    public void SetEndHour(int newEndHour)
    {
        endHour = newEndHour;
    }

    public void UpdateCrashCounter(int newCounterValue, bool needAnimatorEffect = false)
    {
        crashSign.UpdateCounter(newCounterValue, needAnimatorEffect);
    }

    public void UpdateTimeIndicator(int newHour, int newMinutes)
    {
        if (isIndicatorInLockdown) return;

        float temp = (newHour + newMinutes / 60f) / (endHour);

        if (temp >= 0 && temp <= 1)
            timeIndicatorImage.fillAmount = temp;
    }
}
