using UnityEngine;

public class DropdownMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject dropdownPanel;
    [SerializeField] private MenuMaster menuMaster;

    private void Awake()
    {
        ClosePanel();
    }
    public virtual void OpenClosePanel()
    {
        dropdownPanel.SetActive(!dropdownPanel.activeSelf);

        if (dropdownPanel.activeSelf) menuMaster.CloseStuffOnClick();
    }

    public virtual void ClosePanel()
    {
        dropdownPanel.SetActive(false);
    }

    public bool GetState() { return dropdownPanel.activeSelf; }

}
