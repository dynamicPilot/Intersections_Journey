using UnityEngine;

public class ReceiverLevelUI : ReceiverUI
{
    // receiver class for Command Pattern
    [SerializeField] private GameMaster gameMaster;
    [SerializeField] private PauseMenuUI pauseMenuUI;

    public void ChangeGameflow(GameflowCommandType type)
    {
        if (type == GameflowCommandType.startNext) gameMaster.LoadNextLevel();
        else if (type == GameflowCommandType.restart) gameMaster.ReloadLevel();
        else if (type == GameflowCommandType.toMenu) gameMaster.BackToMainMenu();
        else if (type == GameflowCommandType.start) gameMaster.StartLevel();
    }

    public void PauseGame()
    {
        pauseMenuUI.StartPause();
    }

    public void UnpauseGame()
    {
        pauseMenuUI.EndPause();
    }
}
