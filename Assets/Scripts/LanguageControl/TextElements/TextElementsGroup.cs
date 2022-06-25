using UnityEngine;

namespace IJ.LanguageControl.TextElements
{
    public class TextElementsGroup : MonoBehaviour
    {
        [SerializeField] private TextElement[] _elements;
        [SerializeField] private LanguageMaster _master;

        private void Awake()
        {
            _master.OnChangeLanguage += SetLanguageToGroup;
        }

        private void OnDestroy()
        {
            _master.OnChangeLanguage -= SetLanguageToGroup;
        }

        void SetLanguageToGroup(LANG lang)
        {
            foreach (TextElement element in _elements)
            {
                element.SetLanguage(lang);
            }
        }
    }
}
