// Command Pattern classes and interface

using IJ.Core.CommandPattern.Receivers;

namespace IJ.Core.CommandPattern.Commands
{
    public interface ICommand
    {
        void Execute();
    }

    public class PauseUnpauseCommand : ICommand
    {
        ReceiverLevelUI receiver;
        bool toPause;

        public PauseUnpauseCommand(ReceiverLevelUI _receiver, bool _toPause)
        {
            receiver = _receiver;
            toPause = _toPause;
        }

        public void Execute()
        {
            if (receiver == null) return;
            if (toPause) receiver.PauseGame();
            else receiver.UnpauseGame();
        }
    }
}

