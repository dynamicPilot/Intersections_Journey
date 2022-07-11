using UnityEngine;

namespace IJ.AudioControls.Controls
{
    public class MenuAudioControl : AudioControl
    {

        protected override void SetStartTransition()
        {
            _transition.ToMainMenu();
        }
    }
}
