using IJ.Core.UIElements.GameFlowPanels;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using IJ.Utilities.Configs;
using IJ.Animations.Objects;

public class MenusUIControl : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PanelsUI panelsUI;
    [SerializeField] private PauseMenuUI pauseUI;

    [Header("Settings")]
    [SerializeField] private GameConfig config;

    [Header("----- UI -----")]
    [SerializeField] private CrashSign crashSign;
    [SerializeField] private TimeIndicatorAnimation _indicatorAnimation;
    [SerializeField] private Image timeIndicatorImage;

    private int _endHour = 0;
    private float _indicatorUpdateLockdownTimer = 1f;
    bool _isIndicatorInLockdown = false;
    int _counterForIndicator = 3;
    private void Awake()
    {
        _endHour = config.EndHour;
        _indicatorUpdateLockdownTimer = config.IndicatorUpdateTimer;
        _counterForIndicator = 3;

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
        _indicatorAnimation.StartIndicator();
        yield return new WaitForSeconds(_indicatorUpdateLockdownTimer);
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

        if (_counterForIndicator > -1)
        {
            if (_counterForIndicator == 0) _indicatorAnimation.ShowMenuSign();
            _counterForIndicator--;
        }
    }
}
