using AudioControls.SoundPlayers;
using UnityEngine;

namespace IJ.Core.CommandPattern.Receivers
{
    public class ReceiverUI : Singleton<ReceiverUI>
    {
        public enum GameflowCommandType { startNext, restart, toMenu, start }
        public enum MenuCommandType { toSettings, toAccount, quit }

        [SerializeField] private SoundsPlayer soundsPlayer;

        public void MakeClick(int clickSoundIndex)
        {
            if (soundsPlayer == null) return;

            soundsPlayer.PlaySound(clickSoundIndex);
        }

        public virtual void ChangeGameflow(GameflowCommandType type)
        {

        }
    }
}

