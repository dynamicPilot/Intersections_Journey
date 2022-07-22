using UnityEngine;

namespace IJ.Animations.Waves
{
    public interface IAnimationWaveMember
    {
        public void OnWaveStart(AnimationPath path = null);
        public void OnInitialState();
    }

    public class AnimationWavesWithPathRoutine : AbstractAnimationWavesRoutine
    {
        [SerializeField] private AnimationWaveWithPath[] _waves;

        protected override void SetBasicWaves()
        {
            _basicWaves = _waves;
        }

        public void SetWaves(AnimationWaveWithPath[] waves)
        {
            _waves = waves;
        }
    }
}
