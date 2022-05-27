using UnityEngine;

[CreateAssetMenu(fileName = "New SoundCollection", menuName = "Unit/SoundCollection")]
public class SoundCollection: ScriptableObject
{
    [SerializeField] private Sound[] collection;

    public Sound GetSoundOfIndex(int index)
    {
        if (index < collection.Length)
            return collection[index];
        else
            return null;
    }
}
