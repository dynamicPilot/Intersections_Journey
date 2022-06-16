using IJ.Animations;
using IJ.Core.Objects.LevelAndLocation;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.Menus.Main.Levels
{
    [RequireComponent(typeof(LevelPanelView))]
    [RequireComponent(typeof(PanelMoveAnimation))]
    public class LevelsPanelUI : MonoBehaviour, ISetPlayerState
    {
        public enum CROSS { tCross, cross, doubleCross, cross3222 }
       
        [Header("Components")]
        [SerializeField] private MenuFlow _flow;

        private LevelPanelView _view;
        private PanelMoveAnimation _animation;
        private PlayerState _playerState;

        private void Awake()
        {
            _view = GetComponent<LevelPanelView>();
            _animation = GetComponent<PanelMoveAnimation>();
        }

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

        public void LoadLevel(Level level)
        {
            _flow.LoadLevelScene(level);
        }

        
    }
}
