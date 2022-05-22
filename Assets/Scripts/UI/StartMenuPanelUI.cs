using UnityEngine;
using TMPro;

public class StartMenuPanelUI : MenuPanelUI
{
    [Header("Start Menu UI")]
    [SerializeField] private TextMeshProUGUI additional;
    [SerializeField] private string prefix = "<";

    public void SetAdditionalNumber(string additionalText)
    {
        if (additionalText == "") additional.enabled = true;
        else additional.SetText(prefix + additionalText);
    }
}
