using IJ.Utilities.Configs;
using UnityEngine;
using System.IO;

namespace IJ.Core.Settings
{
    public static class SettingsControlUtility
    {
        private static string _fileName = "config.txt";
        public static PlayerConfig ReadData(AudioConfig audioConfig, GameConfig gameConfig)
        {
            string path = Application.persistentDataPath + "/" + _fileName;

            if (!File.Exists(path)) CreateFile(audioConfig, gameConfig, path);
            
            PlayerConfig playerConfig = new PlayerConfig();
            ToFromJsonUtility<PlayerConfig>.LoadJsonFromFile(path, playerConfig);

            return playerConfig;
        }

        public static void SaveData(PlayerConfig playerConfig)
        {
            string path = Application.persistentDataPath + "/" + _fileName;
            ToFromJsonUtility<PlayerConfig>.DumpJsonToFile(path, playerConfig);
        }


        static void CreateFile(AudioConfig audioConfig, GameConfig gameConfig, string path)
        {
            Logging.Log("--- Create new config ---");
            PlayerConfig playerConfig = new PlayerConfig(audioConfig.DefaultMusicVolume, audioConfig.DefaultEffectsVolume,
                audioConfig.DefaultTotalVolume, gameConfig.DefaultLangIndex);
            ToFromJsonUtility<PlayerConfig>.DumpJsonToFile(path, playerConfig);
        }
    }
}
