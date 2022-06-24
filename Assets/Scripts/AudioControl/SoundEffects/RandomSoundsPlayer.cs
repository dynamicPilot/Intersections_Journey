namespace AudioControls.SoundPlayers
{
    public class RandomSoundsPlayer : SoundsPlayer
    {
        public override void PlaySound(int index)
        {
            if (index < 0) index = collection.GetRandomSoundIndex();
            base.PlaySound(index);
        }
    }
}
