using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameMaster : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private PlayerState playerState;
    
    private ISetLevel levelFlow;
    private LoadNextSceneForLevel loadNextSceneForLevel;

    private void Awake()
    {
        Time.timeScale = 0f;
        playerState = GameObject.FindGameObjectWithTag("PlayerState").GetComponent<PlayerState>();

        loadNextSceneForLevel = GetComponent<LoadNextSceneForLevel>();
        levelFlow = GetComponent<LevelFlow>() as ISetLevel;

        if (playerState == null)
        {
            Logging.Log("GameManager: NO PLAYER STATE IN SCENE ---> RETURN TO MENU");
            loadNextSceneForLevel.LoadMainManu();
        }

        level = playerState.GetCurrentLevel();

        if (level == null)
        {
            Logging.Log("GameManager: NO LEVEL IN PLAYER STATE ---> RETURN TO MENU");
            loadNextSceneForLevel.LoadMainManu();
        }
        loadNextSceneForLevel.ContinueLoading();

    }

    private void Start()
    {
        loadNextSceneForLevel.LoadAdditiveSceneAsync(level.SceneName, this);
    }

    public void LevelIsReady()
    {
        levelFlow.SetLevel(level, playerState);        
    }
}
