using IJ.AudioControls.VolumeControls;
using UnityEngine;

namespace IJ.Core.Menus.Others
{
    public class LevelSettingsUI : MonoBehaviour
    {
        [SerializeField] private VolumeButton _totalVolumeButton;

        public void SetSettingsUI()
        {
            _totalVolumeButton.UpdateParameter();
        }

        public bool NeedSaveSattings()
        {
            return _totalVolumeButton.IsStateChange();
        }
    }
}
