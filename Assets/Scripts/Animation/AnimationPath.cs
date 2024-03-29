using UnityEngine;

namespace IJ.Animations
{
    [CreateAssetMenu(fileName = "New AnimationPath", menuName = "Unit/Animations/AnimationPath", order = 0)]
    public class AnimationPath : ScriptableObject
    {
        public Vector3[] Points;
        public AnimationSinglePath[] Paths;

        public Vector3 GetInitialPosition()
        {
            return Paths[0].Points[0];
        }
    }

    [System.Serializable]
    public struct AnimationSinglePath
    {
        public Vector3[] Points;
        public float Duration;
        public int EasyIndex;
    }
}
