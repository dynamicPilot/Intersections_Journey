using IJ.Core.Menus;
using IJ.Core.Menus.Others;
using UnityEngine;

namespace IJ.Core.CommandPattern.Receivers
{
    public class ReceiverLevelUI : ReceiverUI
    {
        [SerializeField] private LevelFlow _levelFlow;
        [SerializeField] private PauseMenuUI pauseMenuUI;
        [SerializeField] private LevelSettingsUI _settingsUI;
        [SerializeField] private SnapshotTransition _snapshotTransition;   

        public override void ChangeGameflow(GameflowCommandType type)
        {
            if (type == GameflowCommandType.startNext) _levelFlow.LoadNextLevel();
            else if (type == GameflowCommandType.restart)
            {
                SaveSettingsIfNeeded();
                _levelFlow.ReloadLevel();
                
            }            
            else if (type == GameflowCommandType.toMenu)
            {
                SaveSettingsIfNeeded();
                _levelFlow.BackToMenu();

            }                
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
            SaveSettingsIfNeeded();
            _snapshotTransition.ToActiveGame();
        }

        void SaveSettingsIfNeeded()
        {
            if (_settingsUI.NeedSaveSattings()) SaveSettings(SaveSettingsCommandType.preferences);
        }
    }
}

