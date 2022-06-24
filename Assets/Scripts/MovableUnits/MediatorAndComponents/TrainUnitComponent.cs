namespace IJ.MovableUnits.MediatorAndComponents
{
    public interface IHoldTrainUnitComponent
    {
        public abstract void DoInStop();
        public abstract void DoInRestart();
    }

    public class TrainUnitComponent : NotifierComponent
    {
        public IHoldTrainUnitComponent _unit;

        public void SetHolder(IHoldTrainUnitComponent unit)
        {
            _unit = unit;
        }

        public void DoInStop()
        {
            _unit.DoInStop();
        }

        public void DoInRestart()
        {
            _unit.DoInRestart();
        }

        //public void DoInEnterCrash()
        //{
        //    _unit.DoInStop();
        //}

        //public void DoInExitCrash()
        //{
        //    _unit.DoInRestart();
        //}
    }
}
