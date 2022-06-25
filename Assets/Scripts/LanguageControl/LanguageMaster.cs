using System.Collections;
using System.Collections.Generic;
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

        public void SetLanguage(LANG lang)
        {
            if (_lang == lang) return;

            _lang = lang;
            if (OnChangeLanguage != null) OnChangeLanguage.Invoke(_lang);
        }
    }
}
