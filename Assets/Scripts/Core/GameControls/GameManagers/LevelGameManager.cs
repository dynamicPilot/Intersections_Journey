using IJ.Core.Managers.GameManagers;
using IJ.Utilities.Configs;
using UnityEngine;

[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(LevelFlow))]
public class LevelGameManager : GameManager
{
    [SerializeField] private ScenesConfig _scenesConfig;
    
    private Level _level;   
    private LoadNextSceneForLevel _loadNextSceneForLevel;

    private void Awake()
    {
        Time.timeScale = 0f;
        FindPlayerState();

        _loadNextSceneForLevel = GetComponent<LoadNextSceneForLevel>();    
        _level = _playerState.GetCurrentLevel();

        if (_level == null)
        {
            Logging.Log("GameManager: NO LEVEL IN PLAYER STATE ---> RETURN TO MENU");
            _flow.BackToMenu();
        }

        _loadNextSceneForLevel.ContinueLoading();

    }

    private void Start()
    {
        int index = _scenesConfig.GetEnvironmentSceneIndexByLevelIndex(_level.LevelIndex);

        if (index == -1) _flow.BackToMenu();
        else _loadNextSceneForLevel.LoadAdditiveSceneAsyncByIndex(index, this);

        //_loadNextSceneForLevel.LoadAdditiveSceneAsync(_level.SceneName, this);
    }

    public void LevelIsReady()
    {
        ISetLevel levelFlow = _flow as ISetLevel;
        levelFlow.SetLevel(_level);        
    }
}
