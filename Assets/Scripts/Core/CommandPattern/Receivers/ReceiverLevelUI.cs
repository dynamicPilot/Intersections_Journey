using UnityEngine;

namespace IJ.Core.CommandPattern.Receivers
{
    public class ReceiverLevelUI : ReceiverUI
    {
        [SerializeField] private LevelFlow _levelFlow;
        [SerializeField] private PauseMenuUI pauseMenuUI;
        [SerializeField] private SnapshotTransition _snapshotTransition;

        public override void ChangeGameflow(GameflowCommandType type)
        {
            if (type == GameflowCommandType.startNext) _levelFlow.LoadNextLevel();
            else if (type == GameflowCommandType.restart) _levelFlow.ReloadLevel();
            else if (type == GameflowCommandType.toMenu) _levelFlow.BackToMenu();
            else if (type == GameflowCommandType.start)
            {
                _levelFlow.StartLevel();
                _snapshotTransition.StartGame();
            }
        }

        public void PauseGame()
        {
            pauseMenuUI.StartPause();
            _snapshotTransition.ToPausedGame();
        }

        public void UnpauseGame()
        {
            pauseMenuUI.EndPause();
            _snapshotTransition.ToActiveGame();
        }
    }
}

