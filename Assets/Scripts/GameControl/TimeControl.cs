using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [Header("Options")]
    //[SerializeField] private int numberOfIntervalsInHour = 4;
    //public int NumberOfIntervalsInHour { get => numberOfIntervalsInHour; }

    [SerializeField] private float gameMinutesPerRealSecond = 15;
    public float GameMinutesFerRealSecond { get => gameMinutesPerRealSecond; }

    [SerializeField] private float secondBetweenTimeUpdate = 1;
    public float SecondBetweenTimeUpdate { get => secondBetweenTimeUpdate; }

    [Header("Day Settings")]

    [SerializeField] private int endHour = 24;
    public int EndHour { get { return endHour; } }

    [Header("Scripts")]
    [SerializeField] private VehicleCreatorControl vehicleCreatorControl;
    [SerializeField] private MenusUIControl menusUIControl;
    [SerializeField] private GameMaster gameMaster;
    [SerializeField] private RepairSitesControl repairSitesControl;

    [Header("----- Info -----")]
    //[SerializeField] private bool gameIsPaused = false;
    //[SerializeField] private float timePlayedOnDay = 0f;


    [SerializeField] private int hour = 0;
    [SerializeField] private int minuteIntervalsIndex = 0;
    [SerializeField] private List<int> minuteIntervals = new List<int>();
    public List<int> MinuteIntervals { get => minuteIntervals; }

    WaitForSeconds timerUpdateInterval;
    Coroutine timeCounterCoroutine;

    private void Awake()
    {
        //gameIsPaused = true;
        int interval = Mathf.FloorToInt(gameMinutesPerRealSecond * secondBetweenTimeUpdate);

        if (60 % interval != 0)
        {
            Logging.Log("TimeControl: DO NOT HAVE RIGHT TIME INTERVAL");
        }

        // calculate intervals
        for (int i = 0; i < 60; i += interval) minuteIntervals.Add(i);       
    }

    public void StartLevel()
    {
        //gameIsPaused = true;
        timerUpdateInterval = new WaitForSeconds(secondBetweenTimeUpdate);

        // set UI
        menusUIControl.SetEndHour(endHour);

        StartDay();
    }

    void StartDay()
    {
        hour = 0;
        minuteIntervalsIndex = 0;
        //menusUIControl.UpdateTimeIndicator(hour, minuteIntervals[minuteIntervalsIndex], true);

        timeCounterCoroutine = StartCoroutine(TimeCounter());
    }

    //private void //Update()
    //{
    //    if (gameIsPaused)
    //    {
    //        return;
    //    }

    //    timePlayedOnDay += Time.deltaTime;

    //    //if (hour - prevHour == state.BetweenHourlyNeedsCalculationTime)
    //    //{
    //    //    //Debug.Log("NEEDS CALCULATION!");
    //    //    prevHour = hour;
    //    //    state.CalculateHourlyProductionAndNeeds(period, hour);
    //    //    levelControl.AddHourToLevelLimitsTimers();
    //    //    dayNightTransition.CheckForMakeTransfer(hour);
    //    //    buildingsControl.ActivateAnimatorsForCosmodromes(hour);
    //    //    dayTimeIndicator.SetValue(hour, period);
    //    //}
    //}

    IEnumerator TimeCounter()
    {
        if (minuteIntervalsIndex >= minuteIntervals.Count)
        {
            // reset index
            minuteIntervalsIndex = 0;
            hour++;
        }
        //hour = Mathf.FloorToInt(timePlayedOnDay * gameMinutesPerRealSecond / 60f);
        vehicleCreatorControl.CheckTime(hour, minuteIntervals[minuteIntervalsIndex]);
        menusUIControl.UpdateTimeIndicator(hour, minuteIntervals[minuteIntervalsIndex]);
        if (repairSitesControl != null) repairSitesControl.CheckRepairSite(hour);

        if (hour >= endHour)
        {
            EndDay();
        }

        yield return timerUpdateInterval;
        
        minuteIntervalsIndex++;

        timeCounterCoroutine = StartCoroutine(TimeCounter());
    }

    void EndDay()
    {
        StopAllCoroutines();
        gameMaster.EndLevel(true);
    }
}
