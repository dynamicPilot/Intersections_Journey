using UnityEngine;

public interface ISetLevel
{
    public void SetLevel(Level level, PlayerState playerState);
}


public class LevelFlow : MonoBehaviour, ISetLevel
{
    [Header("Components")]
    [SerializeField] private MenusUIControl _menusUIControl;
    [SerializeField] private LoadNextSceneForLevel _loadNextSceneForLevel;
    [SerializeField] private LevelControl _levelControl;
    [SerializeField] private CameraControl _cameraControl;
    [SerializeField] private DataSaveAndLoad _dataSaveAndLoad;

    private Level _level;
    private PlayerState _playerState;

    public void SetLevel(Level level, PlayerState playerState)
    {
        _level = level;
        _playerState = playerState;
        StartMenu();
    }

    void StartMenu()
    {
        _menusUIControl.OpenStartMenu(_level.Number, _level.MaxCrashesNumber);
        _levelControl.SetLevel(_level);
    }

    public void StartLevel()
    {
        // start game
        Time.timeScale = 1f;
        _cameraControl.OnLevelStart();
        _levelControl.StartLevel();
    }

    public void ReloadLevel()
    {
        _loadNextSceneForLevel.LoadSceneByName(_level.CrossSceneName);
    }

    public void BackToMainMenu()
    {
        _loadNextSceneForLevel.LoadMainManu();
    }

    public void OpenGameOverMenu()
    {
        _menusUIControl.OpenGameOverMenu(_level.Number);
    }

    public void LoadNextLevel()
    {
        int getNextLevelIndexInLocation = _level.Location.Levels.IndexOf(_level) + 1;
        Logging.Log("LevelFlow: level index is " + getNextLevelIndexInLocation);

        // get next scene name
        if (getNextLevelIndexInLocation > 0 &&
            getNextLevelIndexInLocation < _level.Location.Levels.Count)
        {
            Level nextLevel = _level.Location.Levels[getNextLevelIndexInLocation];
            _playerState.SetCurrentLevel(nextLevel);
            _loadNextSceneForLevel.LoadSceneByName(nextLevel.CrossSceneName);

        }
        else
        {
            BackToMainMenu();
        }
    }

    public void EndLevel(bool isEndingByTime = true, float xPosition = 0f, float yPosition = 0f)
    {
        // stop game
        Time.timeScale = 0f;

        if (isEndingByTime)
        {
            // update data
            int additionalPointsEarnedByLevel = _levelControl.GetFinalPointsNumber() - _playerState.LevelsProgress[_level.LevelIndex].PointsEarned;

            if (additionalPointsEarnedByLevel >= 0)
            {
                bool needSave = false;
                if (!_playerState.LevelsProgress[_level.LevelIndex].IsPassed)
                {
                    _playerState.LevelsProgress[_level.LevelIndex].IsPassed = true;
                    needSave = true;
                }

                if (additionalPointsEarnedByLevel > 0)
                {
                    _playerState.LevelsProgress[_level.LevelIndex].AddPoints(additionalPointsEarnedByLevel);
                    _playerState.LocationsProgress[_level.Location.LocationIndex].AddLevelPoints(_level.LevelIndex, additionalPointsEarnedByLevel);
                    _playerState.AddPointsToTotalNumer(additionalPointsEarnedByLevel);
                    needSave = true;
                }

                // save game
                if (needSave) _dataSaveAndLoad.SaveData(_playerState);
            }

            _menusUIControl.OpenEndLevelMenu(_level.Number);
        }
        else
        {
            // losing
            _cameraControl.OnGameOverEndLevel(new Vector2(xPosition, yPosition));
        }
    }
}
