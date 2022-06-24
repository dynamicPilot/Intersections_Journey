using System.Collections.Generic;
using IJ.MovableUnits.MediatorAndComponents;
using UnityEngine;
using IJ.Utilities;

namespace MovableUnits.Units
{
    public class VEmergencyUnit : VUnit, IHoldEmergencyUnitComponent
    {
        [SerializeField] private RoadMark timerMark;
        [SerializeField] private RectTransform markTransform;
        [SerializeField] private float timerValue;
        [SerializeField] private float allertValue;

        private EmergencyTimer timer;
        private bool needUpdateTimer = false;

        public void DoInRestart()
        {
            needUpdateTimer = false;
            timer.OnStart();
        }

        public void DoInStop()
        {
            needUpdateTimer = true;
            timer.OnStop(transform);
        }

        public override void StartVehicle(List<Path> _paths, int _managerIndex, bool _stopIsParking = false)
        {
            base.StartVehicle(_paths, _managerIndex, _stopIsParking);
            timer = new EmergencyTimer(timerMark, markTransform, timerValue, allertValue);
            timer.OnTimerIsOver += TimerIsOver;
        }

        protected override void StopVehicel()
        {
            base.StopVehicel();
            timer.OnTimerIsOver -= TimerIsOver;
            timer.DestroyTimer();
        }

        void TimerIsOver()
        {
            Logging.Log("Timer is Over");
            info.GameOverState(transform.position);
            timer.OnTimerIsOver -= TimerIsOver;
        }

        protected override void FixedUpdateStuff(float deltaT)
        {
            base.FixedUpdateStuff(deltaT);
            if (needUpdateTimer) timer.UpdateTimer(deltaT);
        }

        protected override void MakeMediatorAndSet()
        {
            MakeEmergencyUnitMediator makeMediator = new MakeEmergencyUnitMediator();
            MakeMediatorBasic(makeMediator);

            IHoldEmergencyUnitComponent holder = this as IHoldEmergencyUnitComponent;
            makeMediator.SetHolderForEmergencyComponent(ref holder);
        }

        public void DoInEnterCrash()
        {
            needUpdateTimer = false;
        }
    }
}


