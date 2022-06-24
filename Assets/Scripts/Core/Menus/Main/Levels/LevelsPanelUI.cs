using IJ.Animations;
using UnityEngine;

namespace IJ.Core.Menus.Main.Levels
{
//    [RequireComponent(typeof(LevelPanelView))]
//    [RequireComponent(typeof(PanelMoveAnimation))]
    public class LevelsPanelUI : MonoBehaviour, ISetPlayerState
    {
        public enum CROSS { tCross, cross, doubleCross, cross3222 }
       
        [Header("Components")]
        [SerializeField] private MenuFlow _flow;
        [SerializeField] private LevelPanelView _view;
        [SerializeField] private PanelMoveAnimation _animation;
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
            _animation.MoveIn();
        }

        public void CloseLocationPage()
        {
            _animation.MoveOut();
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
