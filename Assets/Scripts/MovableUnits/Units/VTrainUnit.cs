using UnityEngine;

namespace MovableUnits.Units
{
    [RequireComponent(typeof(VCrasher))]
    [RequireComponent(typeof(TrainEffects))]
    public class VTrainUnit : VUnit
    {
        public override void EndCrash()
        {
            pauseUpdate = false;
            needUpdateTotalTime = true;

        }
    }
}
