using IJ.MovableUnits.MediatorAndComponents;
using UnityEngine;

namespace MovableUnits.Units
{
    [RequireComponent(typeof(VRepairSiteTagForRepairCar))]
    public class VRepairCarUnit : VUnit
    {
        protected override void MakeMediatorAndSet()
        {
            MakeRepairCarMediator makeMediator = new MakeRepairCarMediator();
            MakeMediatorBasic(makeMediator);

            IHoldRepairCarComponent holder = GetComponent<VRepairSiteTagForRepairCar>() as IHoldRepairCarComponent;
            makeMediator.SetHolderForReapirCarComponent(ref holder);
        }
    }
}



