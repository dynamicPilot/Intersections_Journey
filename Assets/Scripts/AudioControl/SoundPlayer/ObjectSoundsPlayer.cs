namespace AudioControls.SoundPlayers
{
    public class ObjectSoundsPlayer : SoundsPlayer
    {
        private void Awake()
        {
            _source.enabled = false;
        }

        public override void PlaySound(int index)
        {
            _source.enabled = true;
            base.PlaySound(index);
        }

        public override void StopPlaying()
        {
            base.StopPlaying();
            _source.enabled = false;
            
        }
    }
}
