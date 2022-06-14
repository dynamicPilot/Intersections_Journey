namespace IJ.MovableUnits.MediatorAndComponents
{

    public class RepairSiteComponent : NotifierComponent
    {
        public void DoInEnterRepairSite()
        {
            mediator.Notify(this, STATE.inEnterRSite);
        }

        public void DoInExitRepairSite()
        {
            mediator.Notify(this, STATE.inExitRSite);
        }
    }
}