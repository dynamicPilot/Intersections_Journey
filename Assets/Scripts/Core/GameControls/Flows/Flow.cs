using UnityEngine;

public interface ISetPlayerState
{
    public void SetPlayerState(PlayerState playerState);
}

public class Flow : MonoBehaviour, ISetPlayerState
{
    [SerializeField] private protected LoadNextScene _loadNextScene;
    [SerializeField] private protected DataSaveAndLoad _dataSaveAndLoad;

    private protected PlayerState _playerState;
    public void SetPlayerState(PlayerState playerState)
    {
        _playerState = playerState;
    }

    public virtual void LoadLevelScene(Level level)
    {
        _playerState.SetCurrentLevel(level);
        _loadNextScene.LoadSceneByName(level.CrossSceneName);
    }

    public virtual void BackToMenu()
    {

    }
}
