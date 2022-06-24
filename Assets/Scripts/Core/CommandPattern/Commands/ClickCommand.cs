
using IJ.Core.CommandPattern.Receivers;

namespace IJ.Core.CommandPattern.Commands
{
    /// <summary>
    /// Command for click sound.
    /// </summary>
    public class ClickCommand : ICommand
    {
        ReceiverUI receiver;
        int clickSoundIndex;
        public ClickCommand(ReceiverUI _receiver, int _clickSoundIndex)
        {
            receiver = _receiver;
            clickSoundIndex = _clickSoundIndex;
        }

        public void Execute()
        {
            if (receiver == null) return;
            receiver.MakeClick(clickSoundIndex);
        }
    }
}

