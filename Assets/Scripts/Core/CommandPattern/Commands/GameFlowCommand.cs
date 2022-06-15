using IJ.Core.CommandPattern.Receivers;

namespace IJ.Core.CommandPattern.Commands
{
    /// <summary>
    /// Command for game flow type operation, like start level, back to menu, etc.
    /// </summary>
    public class GameFlowCommand : ICommand
    {
        ReceiverUI receiver;
        ReceiverUI.GameflowCommandType type;

        public GameFlowCommand(ReceiverUI _receiver, ReceiverUI.GameflowCommandType _type)
        {
            receiver = _receiver;
            type = _type;
        }

        public void Execute()
        {
            if (receiver == null) return;
            receiver.ChangeGameflow(type);
        }
    }
}

