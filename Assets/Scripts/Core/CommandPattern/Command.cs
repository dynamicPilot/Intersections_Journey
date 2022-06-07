// Command Pattern classes and interface
public interface ICommand
{
    void Execute();
}


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


public class GameFlowCommand : ICommand
{
    ReceiverLevelUI receiver;
    ReceiverUI.GameflowCommandType type;

    public GameFlowCommand(ReceiverLevelUI _receiver, ReceiverUI.GameflowCommandType _type)
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
