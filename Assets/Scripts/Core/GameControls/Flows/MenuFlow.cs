using IJ.Core.Menus.MainMenu;
using UnityEngine;

public class MenuFlow : Flow
{
    [SerializeField] private MenuControl _control;
    [SerializeField] private PanelsControl _panelsControl;
    void LoadData()
    {
        if (_playerState.NeedLoadData)
        {
            _dataSaveAndLoad.LoadData(_playerState);
        }
        
        Time.timeScale = 1f;
    }

    public void SetMenu()
    {
        LoadData();
        _control.SetMenu(_playerState);

        DataSaveAndLoad.DataState state = (DataSaveAndLoad.DataState) PlayerPrefs.GetInt("state");
        if (state == DataSaveAndLoad.DataState.noData)
        {
            // new game
            _control.StartNewGame();
            PlayerPrefs.SetInt("state", (int)DataSaveAndLoad.DataState.fine);
        }
        
    }

    public override void BackToMenu()
    {
        _panelsControl.BackToMenu();
    }

    public override void LoadLevelScene(Level level)
    {
        _panelsControl.TransitToLevelScene();
        base.LoadLevelScene(level);
    }
}
