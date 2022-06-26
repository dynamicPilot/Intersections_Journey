using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RepairSites;
using IJ.Utilities.Configs;

namespace IJ.Core.GameTime
{
    public class TimeControl : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private GameConfig config;

        [Header("Scripts")]
        [SerializeField] private VehicleCreatorControl vehicleCreatorControl;
        [SerializeField] private MenusUIControl menusUIControl;
        [SerializeField] private RepairSitesControl repairSitesControl;

        private int hour = 0;
        private int minuteIntervalsIndex = 0;
        private List<int> minuteIntervals = new List<int>();
        public List<int> MinuteIntervals { get => minuteIntervals; }

        private float _minutesPerRealSecond = 7.5f;
        private float _secondsBetweenTimeUpdate = 2f;
        private int _endHour = 24;
        public int EndHour { get { return _endHour; } }

        WaitForSeconds timerUpdateInterval;
        Coroutine timeCounterCoroutine;

        public delegate void GameOver(Vector3 position, bool endByTimer);
        public event GameOver OnGameOver;

        private void Awake()
        {
            SetConfig();
            SetIntervals();
        }

        void SetConfig()
        {
            _minutesPerRealSecond = config.MinutesForRealSecond;
            _secondsBetweenTimeUpdate = config.SecondsBetweenTimeUpdate;
            _endHour = config.EndHour;
        }

        void SetIntervals()
        {
            int interval = Mathf.FloorToInt(_minutesPerRealSecond * _secondsBetweenTimeUpdate);

            if (60 % interval != 0)
                Logging.Log("TimeControl: DO NOT HAVE RIGHT TIME INTERVAL");

            for (int i = 0; i < 60; i += interval) minuteIntervals.Add(i);
        }

        public void StartLevel()
        {
            //gameIsPaused = true;
            timerUpdateInterval = new WaitForSeconds(_secondsBetweenTimeUpdate);

            StartDay();
        }

        void StartDay()
        {
            hour = 0;
            minuteIntervalsIndex = 0;
            timeCounterCoroutine = StartCoroutine(TimeCounter());
        }

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

            if (hour >= _endHour)
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

            if (OnGameOver != null) OnGameOver.Invoke(new Vector3(0f, 0f, 0f), true);
            //_levelFlow.EndLevel(true);
        }
    }

}
