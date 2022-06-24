using UnityEngine;

namespace AudioControls.SoundPlayers
{
    public class AmbientSoundsPlayer : SoundsPlayer
    {
        [SerializeField] private int _index = 0;

        public void PlayAmbient()
        {
            PlaySound(0);
        }
    }
}
