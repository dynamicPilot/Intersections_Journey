using UnityEngine;

namespace IJ.Animations.Waves
{
    public class AnimationWavesRoutine : AbstractAnimationWavesRoutine
    {
        [SerializeField] private AnimationWave[] _waves;

        protected override void SetBasicWaves()
        {
            _basicWaves = _waves;
        }
    }
}
