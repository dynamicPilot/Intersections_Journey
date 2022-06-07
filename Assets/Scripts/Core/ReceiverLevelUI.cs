using UnityEngine;

public class ReceiverLevelUI : ReceiverUI
{
    // receiver class for Command Pattern
    [SerializeField] private LevelFlow _levelFlow;
    [SerializeField] private PauseMenuUI pauseMenuUI;

    public void ChangeGameflow(GameflowCommandType type)
    {
        if (type == GameflowCommandType.startNext) _levelFlow.LoadNextLevel();
        else if (type == GameflowCommandType.restart) _levelFlow.ReloadLevel();
        else if (type == GameflowCommandType.toMenu) _levelFlow.BackToMainMenu();
        else if (type == GameflowCommandType.start) _levelFlow.StartLevel();
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
