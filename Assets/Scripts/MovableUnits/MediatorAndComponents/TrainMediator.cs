using IJ.Utilities;

namespace IJ.MovableUnits.MediatorAndComponents
{
    public class TrainMediator : Mediator
    {
        private TrainUnitComponent _component;

        public TrainMediator(MoverComponent _moverComponent, EffectsComponent _effectsComponent, TrainUnitComponent
            repairCarComponent) : base(_moverComponent, _effectsComponent)
        {
            _component = repairCarComponent;
            _component.SetMediator(this);
        }


        public override void Notify(object sender, STATE eventCode, Path.TURN turn = Path.TURN.none)
        {
            base.Notify(sender, eventCode, turn);

            if (eventCode == STATE.inStop)
            {
                _component.DoInStop();
            }
            else if (eventCode == STATE.inRestart)
            {
                _component.DoInRestart();
            }

            if (eventCode == STATE.inEnterCrash)
            {
                _component.DoInStop();
            }
            else if (eventCode == STATE.inExitCrash)
            {
                _component.DoInRestart();
            }
        }

    }
}
