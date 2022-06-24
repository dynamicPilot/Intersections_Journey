using IJ.Animations;
using TMPro;
using UnityEngine;

namespace IJ.Core.UIElements
{
    public class PointsUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _pointsText;

        [Header("Components")]
        [SerializeField] private Transform _animationTransform;
        private ISingleActionAnimation _animation;

        private void Awake()
        {
            _animation = _animationTransform.GetComponent<ISingleActionAnimation>();
        }

        public void UpdatePoints(int value)
        {
            _pointsText.SetText(value.ToString());
            _animation.MakeAction();
        }
    }
}

