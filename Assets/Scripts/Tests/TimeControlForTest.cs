using UnityEngine;

namespace IJ.Testing
{
    public class TimeControlForTest : MonoBehaviour
    {
        [SerializeField] private float _timeScale = 1f;

        private void Awake()
        {
            Time.timeScale = _timeScale;
        }
    }
}
