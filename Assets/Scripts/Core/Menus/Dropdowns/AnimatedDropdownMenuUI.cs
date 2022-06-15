using IJ.Animations;
using UnityEngine;

namespace IJ.Core.Menus
{
    public class AnimatedDropdownMenuUI : DropdownMenuUI
    {
        [SerializeField] private HamburgerSignAnimation _animation;

        public override void ClosePanel()
        {
            base.ClosePanel();
            _animation.FromSecondState();
        }

        public override void OpenClosePanel()
        {
            base.OpenClosePanel();

            if (IsInActiveState())
            {
                _animation.ToSecondState();
            }
            else
            {
                _animation.FromSecondState();
            }
        }
    }
}

