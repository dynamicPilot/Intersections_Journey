using AudioControls.SoundPlayers;
using UnityEngine;

public class ReceiverUI : Singleton<ReceiverUI>
{
    public enum GameflowCommandType { startNext, restart, toMenu, start }
    
    [SerializeField] private SoundsPlayer soundsPlayer;

    public void MakeClick(int clickSoundIndex)
    {
        if (soundsPlayer == null) return;

        soundsPlayer.PlaySound(clickSoundIndex);
    }
}


