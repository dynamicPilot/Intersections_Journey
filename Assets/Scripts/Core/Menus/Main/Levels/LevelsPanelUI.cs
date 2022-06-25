using UnityEngine;

namespace IJ.Core.Menus.Main.Levels
{
    public class LevelsPanelUI : MovablePanelUI, ISetPlayerState
    {
        public enum CROSS { tCross, cross, doubleCross, cross3222 }
       
        [Header("Level Components")]
        [SerializeField] private MenuFlow _flow;
        [SerializeField] private LevelPanelView _view;
        [SerializeField] private Transform _levelCards;

        private PlayerState _playerState;

        public void SetPlayerState(PlayerState playerState)
        {
            _playerState = playerState;
            _view.ResetLocationIndex();
        }

        public void OpenLocationPage(Location location)
        {
            _view.OpenLocationPanel(location, _playerState.LevelsProgress, this);
            OpenPage();
        }

        public void HidePanel()
        {
            if (_levelCards.gameObject.activeSelf) _levelCards.gameObject.SetActive(false);
        }

        public void LoadLevel(Level level)
        {
            _flow.LoadLevelScene(level);
            _levelCards.gameObject.SetActive(false);
        }     
    }
}
