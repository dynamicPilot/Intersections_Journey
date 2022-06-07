using TMPro;
using UnityEngine;

public class MenuPanelUI : MonoBehaviour
{
    [Header("Basic UI")]
    [SerializeField] private TextMeshProUGUI header;

    public void SetPanelHeader(string headerText)
    {
        if (headerText == "") header.enabled = true;
        else header.SetText(headerText);
    }
}
