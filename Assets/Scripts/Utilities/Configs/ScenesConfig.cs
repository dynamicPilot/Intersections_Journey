using UnityEngine;

namespace IJ.Utilities.Configs
{
    [CreateAssetMenu(fileName = "New Config", menuName = "Unit/Configs/SceneConfig")]
    public class ScenesConfig : ScriptableObject
    {
        [SerializeField] private LevelScenes[] _scenesByLevels;

        public int GetEnvironmentSceneIndexByLevelIndex(int levelIndex)
        {
            if (levelIndex < _scenesByLevels.Length)
            {
                return _scenesByLevels[levelIndex].EnvironmentSceneIndex;
            }
            else
            {
                return -1;
            }
        }

        public int GetCrossSceneIndexByLevelIndex(int levelIndex)
        {
            if (levelIndex < _scenesByLevels.Length)
            {
                return _scenesByLevels[levelIndex].CrossSceneIndex;
            }
            else
            {
                return -1;
            }
        }
    }

    [System.Serializable]
    public struct LevelScenes
    {
        [SerializeField] private int _levelIndex;
        [SerializeField] private int _environmentSceneIndex;
        [SerializeField] private int _crossSceneIndex;
        [SerializeField] private string _environmentSceneName;
        [SerializeField] private string _crossSceneName;

        public int EnvironmentSceneIndex { get => _environmentSceneIndex; }
        public int CrossSceneIndex { get => _crossSceneIndex; }
    }
}
