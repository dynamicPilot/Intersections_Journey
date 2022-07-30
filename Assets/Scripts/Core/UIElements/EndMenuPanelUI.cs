using IJ.Animations;
using IJ.Animations.Waves;
using UnityEngine;

namespace IJ.Core.UIElements.GameFlowPanels
{
    public class EndMenuPanelUI : MenuPanelUI
    {
        [Header("End Menu UI")]
        [SerializeField] private AnimationWavesRoutine _animationRoutine;
        [SerializeField] private StarsAnimationWaveCreator _starCreator;

        public void SetStarsNumber(int number)
        {
            _starCreator.SetStars(number);
        }

        public void StartAnimationWaves()
        {
            _animationRoutine.StartWaves();
        }
    }
}
