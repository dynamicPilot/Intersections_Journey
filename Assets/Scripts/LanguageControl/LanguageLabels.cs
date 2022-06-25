using UnityEngine;

namespace IJ.LanguageControl.Labels
{
    [System.Serializable]
    public class LanguageLabels
    {
        [Header("Labels : 0 - eng, 1 - rus, 2 - ukr")]
        [SerializeField] private string[] _labels;

        public string GetLabel(LANG lang)
        {
            if ((int)lang > _labels.Length - 1) return _labels[0];
            return _labels[(int)lang];
        }
    }
}
