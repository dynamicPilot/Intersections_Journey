using UnityEngine;
using UnityEngine.UI;

namespace IJ.UIElements
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DisableButtonWithIcon : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CanvasGroup _group;

        [Header("Settings")]
        [SerializeField] private float _disableAlpha = 0.5f;

        public void Interactive(bool state)
        {
            if (state) MakeInteractive();
            else MakeNonInteractive();
        }

        void MakeNonInteractive()
        {
            _button.interactable = false;
            _group.alpha = _disableAlpha;
            _group.interactable = false;
        }

        void MakeInteractive()
        {
            _button.interactable = true;
            _group.alpha = 1f;
            _group.interactable = true;
        }
    }
}
