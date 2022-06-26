using UnityEngine;


namespace IJ.LanguageControl
{
    public enum LANG { eng, rus, ukr}
    public class LanguageMaster : MonoBehaviour
    {
        [SerializeField] private LANG _lang = LANG.eng;
        //public LANG Lang { get => _lang; }

        public delegate void ChangeLanguage(LANG lang);
        public event ChangeLanguage OnChangeLanguage;

        public void SetLanguageByIndex(int langIndex)
        {
            LANG lang = (LANG)langIndex;

            if (_lang == lang) return;

            _lang = lang;
            if (OnChangeLanguage != null) OnChangeLanguage.Invoke(_lang);
        }

        public int GetLanguageIndex()
        {
            return (int)_lang;
        }
    }
}
