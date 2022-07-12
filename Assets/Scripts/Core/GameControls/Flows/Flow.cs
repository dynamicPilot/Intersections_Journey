using IJ.Utilities.Configs;
using UnityEngine;

public interface ISetPlayerState
{
    public void SetPlayerState(PlayerState playerState);
}

public class Flow : MonoBehaviour, ISetPlayerState
{
    [SerializeField] private protected LoadNextScene _loadNextScene;
    [SerializeField] private protected DataSaveAndLoad _dataSaveAndLoad;
    [SerializeField] private protected ScenesConfig _scenesConfig;

    private protected PlayerState _playerState;
    public void SetPlayerState(PlayerState playerState)
    {
        _playerState = playerState;
    }

    public virtual void LoadLevelScene(Level level)
    {
        _playerState.SetCurrentLevel(level);

        int index = _scenesConfig.GetCrossSceneIndexByLevelIndex(level.LevelIndex);

        if (index == -1) return;
        else _loadNextScene.LoadSceneByIndex(index);

        //_loadNextScene.LoadSceneByName(level.CrossSceneName);
    }

    public virtual void BackToMenu()
    {

    }
}
