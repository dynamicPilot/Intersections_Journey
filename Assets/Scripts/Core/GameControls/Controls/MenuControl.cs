using IJ.Animations.Waves;
using IJ.Core.Menus.Main.Levels;
using IJ.Core.Menus.MainMenu;
using UnityEngine;

public class MenuControl : SceneControl, IOnEndWaves
{
    [Header("Components")]
    [SerializeField] private MenuMaster _menuMaster;
    [SerializeField] private PointsControl _pointsControl;
    [SerializeField] private MapLocationsUI _mapLocationsUI;
    [SerializeField] private LevelsPanelUI _levelsPanelUI;

    [Header("New Game Animation")]
    [SerializeField] private AnimationWavesRoutine _routine;

    public void OnEndWaves()
    {
        StartTutorial();
    }

    public void SetMenu(PlayerState playerState)
    {
        _pointsControl.SetPlayerState(playerState);
        _mapLocationsUI.SetPlayerState(playerState);
        _levelsPanelUI.SetPlayerState(playerState);
        _menuMaster.SetPlayerState(playerState);
    }

    public void StartNewGame()
    {
        _routine.StartWaves(this);
    }


}
