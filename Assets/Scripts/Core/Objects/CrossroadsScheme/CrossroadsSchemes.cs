using UnityEngine;

namespace IJ.Core.Objects.Schemes
{
    [CreateAssetMenu(fileName = "New Schemes", menuName = "Unit/Schemes", order = 0)]
    [System.Serializable]
    public class CrossroadsSchemes : ScriptableObject
    {
        [Header("Crossroads Schemes")]
        public Sprite _tCross;
        public Sprite _cross;
        public Sprite _doubleCross;
        public Sprite _cross3222;
    }
}
