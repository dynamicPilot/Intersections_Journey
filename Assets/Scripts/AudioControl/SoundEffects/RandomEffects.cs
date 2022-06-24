using AudioControls.Commons;
using UnityEngine;

namespace AudioControls.SoundEffects
{
    public class RandomEffects : EnvironmentEffects
    {
        [SerializeField] private AudioPlayer _player;
        public override void PlayEffect()
        {
           _player.PlaySound(-1);
        }
    }
}
