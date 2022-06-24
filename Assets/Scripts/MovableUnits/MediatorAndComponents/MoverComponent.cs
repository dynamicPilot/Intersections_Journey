namespace IJ.MovableUnits.MediatorAndComponents
{

    public interface IHoldMoverComponent
    {
        public abstract void SetMoverComponent(MoverComponent moverComponent);
        public abstract void ChangeIsInTurn(bool isInTurn);
        public abstract void ChangeIsInRepairSite(bool isInRepairSite);
        public abstract void ChangeIsInCrash(bool isInCrash);
    }

    public class MoverComponent : NotifierComponent
    {
        IHoldMoverComponent _holder;

        public void SetHolder(IHoldMoverComponent holder)
        {
            _holder = holder;
        }

        public void DoInStop()
        {
            mediator.Notify(this, STATE.inStop);
        }

        public void DoInRestart()
        {
            mediator.Notify(this, STATE.inRestart);
        }

        public void DoInTurn()
        {
            _holder.ChangeIsInTurn(true);
        }

        public void DoEndTurn()
        {
            _holder.ChangeIsInTurn(false);
        }

        public void DoInEnterRepairSite()
        {
            _holder.ChangeIsInRepairSite(true);
        }

        public void DoInExitRepairSite()
        {
            _holder.ChangeIsInRepairSite(false);
        }

        public void DoInEnterCrash()
        {
            _holder.ChangeIsInCrash(true);
        }

        public void DoInExitCrash()
        {
            _holder.ChangeIsInCrash(false);
        }
    }
}


