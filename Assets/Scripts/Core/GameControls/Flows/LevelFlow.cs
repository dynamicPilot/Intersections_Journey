using IJ.Core.CameraControls;
using UnityEngine;

public interface ISetLevel
{
    public void SetLevel(Level level);
}


public class LevelFlow : Flow, ISetLevel
{
    [Header("Components")]
    [SerializeField] private MenusUIControl _menusUIControl;
    [SerializeField] private LevelControl _levelControl;
    [SerializeField] private CameraControl _cameraControl;

    private Level _level;

    public void SetLevel(Level level)
    {
        _level = level;
        SetMenus();
    }

    void SetMenus()
    {
        _menusUIControl.OpenStartMenu(_level.Number, _level.MaxCrashesNumber);
        _levelControl.SetLevel(_level);
    }

    public void StartLevel()
    {
        Time.timeScale = 1f;
        _cameraControl.OnLevelStart();
        _levelControl.StartLevel();
    }

    public void ReloadLevel()
    {
        _loadNextScene.LoadSceneByName(_level.CrossSceneName);
    }

    public override void BackToMenu()
    {
        _loadNextScene.LoadMainManu();
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
            LoadLevelScene(nextLevel);
        }
        else
        {
            BackToMenu();
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
                if (additionalPointsEarnedByLevel > 0)
                {
                    _playerState.LevelsProgress[_level.LevelIndex].AddPoints(additionalPointsEarnedByLevel);
                    _playerState.LocationsProgress[_level.Location.LocationIndex].AddLevelPoints(_level.LevelIndex, additionalPointsEarnedByLevel);
                    _playerState.AddPointsToTotalNumber(additionalPointsEarnedByLevel);
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
            _menusUIControl.OpenGameOverMenu(_level.Number);
            _cameraControl.OnGameOverEndLevel(new Vector2(xPosition, yPosition));
        }
    }
}
