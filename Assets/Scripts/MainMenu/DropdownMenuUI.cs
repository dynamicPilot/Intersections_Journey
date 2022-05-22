using UnityEngine;

public class DropdownMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject dropdownPanel;


    private void Awake()
    {
        ClosePanel();
    }
    public virtual void OpenClosePanel()
    {
        dropdownPanel.SetActive(!dropdownPanel.activeSelf);
    }

    public virtual void ClosePanel()
    {
        dropdownPanel.SetActive(false);
    }

    public bool GetState() { return dropdownPanel.activeSelf; }

}
