using IJ.Utilities;

namespace IJ.MovableUnits.MediatorAndComponents
{
    public interface IHoldEffectsComponent
    {
        public abstract void ChangeIsInTurn(bool _isInTurn, Path.TURN turn);
    }

    public class EffectsComponent : NotifierComponent
    {
        IHoldEffectsComponent _holder;

        public void SetHolder(IHoldEffectsComponent holder)
        {
            _holder = holder;
        }

        public void DoInTurn(Path.TURN turn)
        {
            _holder.ChangeIsInTurn(true, turn);
        }

        public void DoEndTurn()
        {
            _holder.ChangeIsInTurn(false, Path.TURN.none);
        }
    }
}

