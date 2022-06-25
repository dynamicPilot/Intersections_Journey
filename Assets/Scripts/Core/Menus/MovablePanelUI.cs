using IJ.Animations;
using UnityEngine;

namespace IJ.Core.Menus
{
    public class MovablePanelUI : MonoBehaviour
    {
        [SerializeField] private PanelMoveAnimation _animation;
        bool _isOpen = false;
        public virtual void OpenPage()
        {
            _animation.MoveIn();
            _isOpen = true;
        }

        public virtual void ClosePage()
        {
            if (!_isOpen) return;

            _animation.MoveOut();
            _isOpen = false;
        }
    }
}
