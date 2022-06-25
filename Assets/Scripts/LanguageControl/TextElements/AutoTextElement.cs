using UnityEngine;

namespace IJ.LanguageControl.TextElements
{
    public class AutoTextElement : TextElement
    {
        [SerializeField] private LanguageMaster _master;

        private void Awake()
        {
            _master.OnChangeLanguage += SetLanguage;
        }

        private void OnDestroy()
        {
            _master.OnChangeLanguage -= SetLanguage;
        }
    }
}
