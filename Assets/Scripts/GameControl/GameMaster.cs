using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameMaster : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private MenusUIControl menusUIControl;

    [Header("Scripts")]
    [SerializeField] private PlayerState playerState;
    [SerializeField] private LevelControl levelControl;
    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private LoadNextSceneForLevel loadNextSceneForLevel;
    [SerializeField] private DataSaveAndLoad dataSaveAndLoad;
    

    private void Awake()
    {
        Time.timeScale = 0f;
        playerState = GameObject.FindGameObjectWithTag("PlayerState").GetComponent<PlayerState>();

        if (playerState == null)
        {
            Logging.Log("GameManager: NO PLAYER STATE IN SCENE!");
        }

        level = playerState.GetCurrentLevel();
        loadNextSceneForLevel.ContinueLoading();

    }

    private void Start()
    {
        loadNextSceneForLevel.LoadAdditiveSceneAsync(level.SceneName);
        //SetNewLevel();
    }
    public void SetNewLevel()
    {
        if (level != null)
        {           
            menusUIControl.OpenStartMenu(level.Number, level.MaxCrashesNumber);
            levelControl.SetLevel(level);

        }
        else
        {
            Logging.Log("GameMaster: no level");
        }           
    }

    public void StartLevel()
    {
        // start game
        Time.timeScale = 1f;
        cameraControl.OnLevelStart();
        levelControl.StartLevel();
    }

    public void EndLevel(bool isEndingByTime = true, float xPosition = 0f, float yPosition = 0f)
    {
        // stop game
        Time.timeScale = 0f;

        if (isEndingByTime)
        {
            // winning
            Logging.Log("GameManager: WIN!");

            // update data
            int additionalPointsEarnedByLevel = levelControl.GetFinalPointsNumber() - playerState.LevelsProgress[level.LevelIndex].PointsEarned;
            
            if (additionalPointsEarnedByLevel >= 0)
            {
                bool needSave = false;
                if (!playerState.LevelsProgress[level.LevelIndex].IsPassed)
                {
                    playerState.LevelsProgress[level.LevelIndex].IsPassed = true;
                    needSave = true;
                }
                
                if (additionalPointsEarnedByLevel > 0)
                {
                    playerState.LevelsProgress[level.LevelIndex].AddPoints(additionalPointsEarnedByLevel);
                    playerState.LocationsProgress[level.Location.LocationIndex].AddLevelPoints(level.LevelIndex, additionalPointsEarnedByLevel);
                    playerState.AddPointsToTotalNumer(additionalPointsEarnedByLevel);
                    needSave = true;
                }
                
                // save game
                if (needSave) dataSaveAndLoad.SaveData(playerState);
            }

            menusUIControl.OpenEndLevelMenu(level.Number);
        }
        else
        {
            // losing
            Logging.Log("GameManager: LOSE!");
            cameraControl.OnGameOverEndLevel(new Vector2(xPosition, yPosition));
        }
    }

    public void ReloadLevel()
    {
        loadNextSceneForLevel.LoadSceneByName(level.CrossSceneName);
    }

    public void BackToMainMenu()
    {
        loadNextSceneForLevel.LoadMainManu();
    }

    public void LoadNextLevel()
    {

    }

    public void OpenGameOverMenu()
    {
        menusUIControl.OpenGameOverMenu(level.Number);
    }
}
