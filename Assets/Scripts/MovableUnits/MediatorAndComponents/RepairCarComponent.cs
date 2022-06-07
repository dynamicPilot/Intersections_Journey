using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovableUnits.MediatorAndComponents
{

    public interface IHoldRepairCarComponent
    {
        public void DoInStop();
    }

    public class RepairCarComponent : NotifierComponent
    {
        IHoldRepairCarComponent _holder;
        public void SetHolder(IHoldRepairCarComponent holder)
        {
            _holder = holder;
        }
        public void DoInStop()
        {
            _holder.DoInStop();
        }
    }

}