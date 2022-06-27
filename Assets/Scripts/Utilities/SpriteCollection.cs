using UnityEngine;

namespace IJ.Utilities
{
    [CreateAssetMenu(fileName = "New SpriteCollection", menuName = "Unit/Sprite Collection")]
    public class SpriteCollection : ScriptableObject
    {
        public Sprite[] Collection;
    }
}
