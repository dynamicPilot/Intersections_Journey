using IJ.Core.CommandPattern.Receivers;

namespace IJ.Core.CommandPattern.Commands
{
    public class SaveSettingsCommand : ICommand
    {
        ReceiverUI _receiver;
        ReceiverUI.SaveSettingsCommandType _type;

        public SaveSettingsCommand(ReceiverUI receiver, ReceiverUI.SaveSettingsCommandType type)
        {
            this._receiver = receiver;
            this._type = type;
        }

        public void Execute()
        {
            if (_receiver == null) return;
            _receiver.SaveSettings(_type);
        }
    }
}
