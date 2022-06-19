using IJ.Core.UIElements.GameFlowPanels;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utilites.Configs;

public class MenusUIControl : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PanelsUI panelsUI;
    [SerializeField] private PauseMenuUI pauseUI;

    [Header("Settings")]
    [SerializeField] private GameConfig config;

    [Header("----- UI -----")]
    [SerializeField] private CrashSign crashSign;
    [SerializeField] private Image timeIndicatorImage;
    [SerializeField] private Animator indicatorAnimator;

    private int _endHour = 0;
    private float _indicatorUpdateLockdownTimer = 0.92f;
    bool _isIndicatorInLockdown = false;

    private void Awake()
    {
        _endHour = config.EndHour;
        _indicatorUpdateLockdownTimer = config.IndicatorUpdateTimer;

        SetUI();
    }

    void SetUI()
    {
        UpdateCrashCounter(0);
        StartCoroutine(IndicatorUpdateLockdown());
    }

    IEnumerator IndicatorUpdateLockdown()
    {
        _isIndicatorInLockdown = true;
        yield return new WaitForSeconds(_indicatorUpdateLockdownTimer);
        indicatorAnimator.enabled = false;
        _isIndicatorInLockdown = false;
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

    public void UpdateCrashCounter(int newCounterValue, bool needAnimatorEffect = false)
    {
        crashSign.UpdateCounter(newCounterValue, needAnimatorEffect);
    }

    public void UpdateTimeIndicator(int newHour, int newMinutes)
    {
        if (_isIndicatorInLockdown) return;

        float temp = (newHour + newMinutes / 60f) / (_endHour);

        if (temp >= 0 && temp <= 1)
            timeIndicatorImage.fillAmount = temp;
    }
}
