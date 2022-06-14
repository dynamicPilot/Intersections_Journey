using IJ.Utilities;

namespace IJ.MovableUnits.MediatorAndComponents
{
    public enum STATE { inStop, inRestart, startTurn, endTurn, inEnterRSite, inExitRSite }

    public interface IMediator
    {
        void Notify(object sender, STATE eventCode, Path.TURN turn = Path.TURN.none);
    }

    public class Mediator : IMediator
    {
        private MoverComponent moverComponent;
        private EffectsComponent effectsComponent;
        public Mediator(MoverComponent _moverComponent, EffectsComponent _effectsComponent)
        {
            moverComponent = _moverComponent;
            moverComponent.SetMediator(this);

            effectsComponent = _effectsComponent;
            effectsComponent.SetMediator(this);
        }

        public virtual void Notify(object sender, STATE eventCode, Path.TURN turn = Path.TURN.none)
        {
            if (eventCode == STATE.startTurn)
            {
                moverComponent.DoInTurn();
                effectsComponent.DoInTurn(turn);
            }
            else if (eventCode == STATE.endTurn)
            {
                moverComponent.DoEndTurn();
                effectsComponent.DoEndTurn();
            }

            if (eventCode == STATE.inEnterRSite)
            {
                moverComponent.DoInEnterRepairSite();
            }
            else if (eventCode == STATE.inExitRSite)
            {
                moverComponent.DoInExitRepairSite();
            }
        }
    }

    public class EmergencyUnitMediator : Mediator
    {
        private EmergencyUnitComponent _emergencyComponent;

        public EmergencyUnitMediator(MoverComponent _moverComponent, EffectsComponent _effectsComponent, EmergencyUnitComponent emergencyComponent) : base(_moverComponent, _effectsComponent)
        {
            _emergencyComponent = emergencyComponent;
            _emergencyComponent.SetMediator(this);
        }

        public override void Notify(object sender, STATE eventCode, Path.TURN turn = Path.TURN.none)
        {
            base.Notify(sender, eventCode, turn);

            if (eventCode == STATE.inStop)
            {
                _emergencyComponent.DoInStop();
            }
            else if (eventCode == STATE.inRestart)
            {
                _emergencyComponent.DoInRestart();
            }
        }
    }


    public class RepairCarMediator: Mediator
    {
        private RepairCarComponent _repairCarComponent;

        public RepairCarMediator(MoverComponent _moverComponent, EffectsComponent _effectsComponent, RepairCarComponent
            repairCarComponent) : base(_moverComponent, _effectsComponent)
        {
            _repairCarComponent = repairCarComponent;
            _repairCarComponent.SetMediator(this);
        }


        public override void Notify(object sender, STATE eventCode, Path.TURN turn = Path.TURN.none)
        {
            base.Notify(sender, eventCode, turn);
            if (eventCode == STATE.inStop)
            {
                _repairCarComponent.DoInStop();
            }
        }
    }


}

