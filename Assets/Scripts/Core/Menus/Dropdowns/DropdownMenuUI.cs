using IJ.Core.Menus.MainMenu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IJ.Core.Menus
{
    public class DropdownMenuUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject dropdownPanel;
        [SerializeField] private PanelsControl _panelsControl;

        private void Awake()
        {
            ClosePanel();
        }

        public virtual void OpenClosePanel()
        {
            dropdownPanel.SetActive(!dropdownPanel.activeSelf);

            if (dropdownPanel.activeSelf) _panelsControl.FlipInvertedLocationOver();
        }

        public virtual void ClosePanel()
        {
            dropdownPanel.SetActive(false);
        }

        public bool IsInActiveState() { return dropdownPanel.activeSelf; }

        public void OnPointerClick(PointerEventData eventData)
        {
            OpenClosePanel();
        }
    }
}
