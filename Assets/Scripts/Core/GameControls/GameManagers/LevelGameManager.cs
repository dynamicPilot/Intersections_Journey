using IJ.Core.Managers.GameManagers;
using UnityEngine;

[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(LevelFlow))]
public class LevelGameManager : GameManager
{
    [SerializeField] private Level _level;
   
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
        _loadNextSceneForLevel.LoadAdditiveSceneAsync(_level.SceneName, this);
    }

    public void LevelIsReady()
    {
        ISetLevel levelFlow = _flow as ISetLevel;
        levelFlow.SetLevel(_level);        
    }
}
