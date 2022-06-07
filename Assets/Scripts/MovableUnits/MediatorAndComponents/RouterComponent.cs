namespace MovableUnits.MediatorAndComponents
{
    public class RouterComponent : NotifierComponent
    {
        public void DoInTurn(Path.TURN turn)
        {
            mediator.Notify(this, STATE.startTurn, turn);
        }
        public void DoEndTurn()
        {
            mediator.Notify(this, STATE.endTurn);
        }
    }
}