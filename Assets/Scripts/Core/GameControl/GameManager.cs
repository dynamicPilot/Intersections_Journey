using UnityEngine;

//[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    [SerializeField] LoadNextScene loadNextScene;
    [SerializeField] DataSaveAndLoad dataSaveAndLoad;
    [SerializeField] LevelsAndLocationsManager levelsAndLocationsManager;
    [SerializeField] private MenuMaster menuMaster;

    [SerializeField] PlayerState playerState;

    private void Awake()
    {
        loadNextScene = GetComponent<LoadNextScene>();
        playerState = GameObject.FindGameObjectWithTag("PlayerState").GetComponent<PlayerState>();

        if (playerState == null)
        {
            Logging.Log("GameManager: NO PLAYER STATE IN SCENE!");
        }
    }

    private void Start()
    {
        if (playerState.NeedLoadData) dataSaveAndLoad.LoadData(playerState);
        menuMaster.SetMenu(playerState);
        Time.timeScale = 1f;
        //dataSaveAndLoad.SaveData(playerState);
    }

    public void LoadLevel(Level level)
    {
        playerState.SetCurrentLevel(level);
        loadNextScene.LoadSceneByName(level.CrossSceneName);
    }
}
