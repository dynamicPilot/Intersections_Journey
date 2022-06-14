namespace IJ.MovableUnits.MediatorAndComponents
{
    public interface IHoldEmergencyUnitComponent
    {
        public abstract void DoInStop();
        public abstract void DoInRestart();
    }

    public class EmergencyUnitComponent : NotifierComponent
    {
        IHoldEmergencyUnitComponent unit;

        public void SetHolder(IHoldEmergencyUnitComponent _unit)
        {
            unit = _unit;
        }

        public void DoInStop()
        {
            unit.DoInStop();
        }

        public void DoInRestart()
        {
            unit.DoInRestart();
        }
    }
}

