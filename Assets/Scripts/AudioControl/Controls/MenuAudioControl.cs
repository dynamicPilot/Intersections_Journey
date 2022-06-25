using UnityEngine;

namespace IJ.AudioControls.Controls
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
