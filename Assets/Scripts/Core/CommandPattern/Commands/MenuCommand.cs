using IJ.Core.CommandPattern.Receivers;

namespace IJ.Core.CommandPattern.Commands
{
    /// <summary>
    /// Command for Menu type actions.
    /// </summary>
    public class MenuCommand : ICommand
    {
        ReceiverMenuUI receiver;
        ReceiverUI.MenuCommandType type;

        public MenuCommand(ReceiverMenuUI _receiver, ReceiverUI.MenuCommandType _type)
        {
            receiver = _receiver;
            type = _type;
        }

        public void Execute()
        {
            if (receiver == null) return;
            receiver.MakeMenuCommand(type);
        }
    }
}
