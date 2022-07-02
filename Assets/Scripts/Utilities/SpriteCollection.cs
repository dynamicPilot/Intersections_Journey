using UnityEngine;

namespace IJ.Utilities
{
    [CreateAssetMenu(fileName = "New SpriteCollection", menuName = "Unit/Collections/Sprite Collection")]
    public class SpriteCollection : ScriptableObject
    {
        public Sprite[] Collection;
    }
}
