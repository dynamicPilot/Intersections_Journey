namespace IJ.Utilities.Configs
{
    [System.Serializable]
    public class PlayerConfig
    {
        public float MusicVolume;
        public float EffectsVolume;
        public float TotalVolume;
        public int LangIndex;

        public PlayerConfig()
        {
            MusicVolume = 0;
            EffectsVolume = 0;
            TotalVolume = 0;
            LangIndex = 0;
        }

        public PlayerConfig(float musicVolume, float effectsVolume, float totalVolume, int langIndex = 0)
        {
            MusicVolume = musicVolume;
            EffectsVolume = effectsVolume;
            TotalVolume = totalVolume;
            LangIndex = langIndex;
        }
    }
}
