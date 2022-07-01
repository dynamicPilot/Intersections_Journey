using IJ.Animations;
using UnityEngine;
using AudioControls.SoundPlayers;

namespace IJ.Core.Menus.Others
{
    public class ErrorPageControl : MonoBehaviour
    {
        [SerializeField] private CanvasGroupDesolveAndAppearAnimation _animation;
        [SerializeField] private SoundsPlayerWithDelay _player;

        [Header("Settings")]
        [SerializeField] private float _soundDelay = 1f;
        [SerializeField] private int _soundIndex;

        public void OpenPage()
        {
            _animation.Appear();
            if (_player != null) _player.PlaySoundWithDelay(_soundIndex, _soundDelay);
        }

        public void ClosePage()
        {
            _animation.Desolve(true);
        }
    }
}
