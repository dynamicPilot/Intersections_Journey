using UnityEngine;

namespace AudioControls.Commons
{
    [CreateAssetMenu(fileName = "New SoundCollection", menuName = "Unit/Collections/SoundCollection")]
    public class SoundCollection : ScriptableObject
    {
        [SerializeField] private Sound[] collection;

        [Header("Random from index ( 0 for all lenght)")]
        [SerializeField] private int _randomFrom = 0;
        [Header("Random to index ( -1 for all lenght)")]
        [SerializeField] private int _randomTo = -1;
        public Sound GetSoundOfIndex(int index)
        {
            if (index < collection.Length)
                return collection[index];
            else
                return null;
        }

        public Sound GetRandomSound()
        {
            return collection[GetRandomSoundIndex()];
        }

        public int GetRandomSoundIndex()
        {
            int start = _randomFrom;
            int end = (_randomTo + 1 == 0) ? collection.Length : _randomTo + 1;
            return Random.Range(start, end);
        }
    }
}

