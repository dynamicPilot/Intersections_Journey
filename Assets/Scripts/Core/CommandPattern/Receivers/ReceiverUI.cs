using AudioControls.SoundPlayers;
using IJ.Core.Settings;
using UnityEngine;

namespace IJ.Core.CommandPattern.Receivers
{
    public class ReceiverUI : Singleton<ReceiverUI>
    {
        public enum GameflowCommandType { startNext, restart, toMenu, start }
        public enum MenuCommandType { toSettings, toCredits, quit }
        public enum SaveSettingsCommandType { preferences, account }

        [SerializeField] private SoundsPlayer soundsPlayer;
        [SerializeField] private SettingsControl _settings;
        public void MakeClick(int clickSoundIndex)
        {
            if (soundsPlayer == null) return;

            soundsPlayer.PlaySound(clickSoundIndex);
        }

        public virtual void ChangeGameflow(GameflowCommandType type)
        {

        }

        public virtual void SaveSettings(SaveSettingsCommandType type)
        {
            if (type == SaveSettingsCommandType.preferences) _settings.SavePreferences();
        }
    }
}


