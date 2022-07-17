using UnityEngine;

namespace IJ.Animations.Helper
{
    public class GameObjectAnimationHelper : AnimationHelper
    {
        [SerializeField] private Transform[] _points;

        private void Start()
        {
            DoTestScript();
        }

        protected override Vector3[] GetPoints()
        {
            Vector3[] result = new Vector3[_points.Length];
            for (int i = 0; i < _points.Length; i++)
            {
                result[i] = _points[i].position;
            }

            return result;
        }
    }
}
