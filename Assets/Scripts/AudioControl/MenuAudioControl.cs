using UnityEngine;

namespace AudioControls
{
    public class MenuAudioControl : AudioControl
    {
        [SerializeField] private SnapshotTransition _transition;

        public override void SetAudio()
        {
            base.SetAudio();
            _transition.ToStartGame();
        }
    }
}
