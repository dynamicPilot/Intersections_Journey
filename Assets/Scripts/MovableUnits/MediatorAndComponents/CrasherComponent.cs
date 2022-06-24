namespace IJ.MovableUnits.MediatorAndComponents
{
    public class CrasherComponent : NotifierComponent
    {
        public void DoInEnterCrash()
        {
            mediator.Notify(this, STATE.inEnterCrash);
        }

        public void DoInExitCrash()
        {
            mediator.Notify(this, STATE.inExitCrash);
        }
    }
}
