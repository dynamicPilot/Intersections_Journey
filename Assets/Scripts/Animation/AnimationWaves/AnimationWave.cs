namespace IJ.Animations.Waves
{
    [System.Serializable]
    public class AnimationWave : AbstractAnimationWave
    {
        public override void SetInitialState()
        {
            if (_members == null || _members.Length < 1) GetMemebersFromObjects();

            for (int i = 0; i < _members.Length; i++)
            {
                _members[i].OnInitialState();
            }
        }

        public override void StartWave()
        {
            if (_members == null || _members.Length < 1) GetMemebersFromObjects();

            for (int i = 0; i < _members.Length; i++)
            {
                _members[i].OnWaveStart();
            }
        }
    }
}
