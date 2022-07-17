using IJ.Animations.Waves;
using UnityEngine;

public class GameStartFlow : Flow, IOnEndWaves
{
    [SerializeField] private AnimationWavesWithPathRoutine _mainRoutine;
    [SerializeField] private AnimationWavesWithPathRoutine _newGameRoutine;
    [SerializeField] private bool _isNewGame = false;

    DataSaveAndLoad.DataState _state;
    public void SetGameStart()
    {
        _mainRoutine.StartWaves(this);
        _state = DataSaveAndLoad.DataState.error;
        LoadData();
    }

    private void LoadData()
    {
        if (_playerState.NeedLoadData)
        {
            _state = _dataSaveAndLoad.LoadData(_playerState);

            PlayerPrefs.SetInt("state", (int)_state);
            if (_isNewGame) PlayerPrefs.SetInt("state", (int)DataSaveAndLoad.DataState.noData);
        }
    }

    private void StartNewGame()
    {
        _newGameRoutine.StartWaves();
    }

    public override void BackToMenu()
    {
        _loadNextScene.LoadMainManu();
    }

    public void OnEndWaves()
    {
        if (_state == DataSaveAndLoad.DataState.noData) StartNewGame();
        else if (_state == DataSaveAndLoad.DataState.fine) BackToMenu();
        else
        {
            Logging.Log("ERROR ON LOAD!");
        }
    }
}
