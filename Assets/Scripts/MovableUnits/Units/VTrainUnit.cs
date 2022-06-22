using IJ.MovableUnits.MediatorAndComponents;
using IJ.Utilities;
using MovableUnits.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace MovableUnits.Units
{
    [RequireComponent(typeof(VCrasher))]
    [RequireComponent(typeof(TrainEffects))]
    [RequireComponent(typeof(VTrainSound))]
    public class VTrainUnit : VUnit
    {
        public override void StartVehicle(List<Path> _paths, int _managerIndex, bool _stopIsParking = false)
        {
            base.StartVehicle(_paths, _managerIndex, _stopIsParking);
            GetComponent<VTrainSound>().DoInStartRoute();
        }

        protected override void StopVehicel()
        {
            GetComponent<VTrainSound>().DoInStop();
            base.StopVehicel();
        }

        public override void EndCrash()
        {
            pauseUpdate = false;
            needUpdateTotalTime = true;
        }

        protected override void MakeMediatorAndSet()
        {
            MakeTrainMediator makeMediator = new MakeTrainMediator();
            MakeMediatorBasic(makeMediator);

            IHoldTrainUnitComponent holder = GetComponent<VTrainSound>() as IHoldTrainUnitComponent;
            makeMediator.SetHolderForTrainUnitComponent(ref holder);
        }
    }
}
