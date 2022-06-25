using IJ.LanguageControl.Labels;
using TMPro;
using UnityEngine;

namespace IJ.LanguageControl.TextElements
{
    public class TextElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private LanguageLabels _labels;

        public void SetLanguage(LANG lang)
        {
            _text.SetText(_labels.GetLabel(lang));
        }
    }
}
