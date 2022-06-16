using IJ.Core.Objects.LevelAndLocation;
using IJ.Core.Objects.Schemes;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.Menus.Main.Levels
{
    public class LevelPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject[] _backgroundsForLocations;
        [SerializeField] private List<LevelUI> levelUIs;

        [Header("Crossroads Schemas")]
        [SerializeField] private CrossroadsSchemes _schemes;

        [Header("Schemas Colors")]
        [SerializeField] private Color[] _colors;

        private GameObject _activeBackground = null;
        private SchemesReader _reader;

        private int _locationIndex = -1;
        private void Awake()
        {
            ResetLocationIndex();
            _reader = new SchemesReader(_schemes);
        }

        public void ResetLocationIndex()
        {
            _locationIndex = -1;
        }

        public void OpenLocationPanel(Location location, List<LocationOrLevelProgress> progress, LevelsPanelUI levelsUI)
        {
            if (_locationIndex == location.LocationIndex) return;

            ChangeBackground(location.LocationIndex);
            _locationIndex = location.LocationIndex;

            int index = 0;

            for (int i = 0; i < location.Levels.Count; i++)
            {
                if (i < levelUIs.Count)
                {
                    Level level = location.Levels[i];
                    levelUIs[i].SetLevelUI(level, levelsUI, progress[level.LevelIndex], _reader.GetSprite(level.CrossType), GetColor(i));
                    levelUIs[i].gameObject.SetActive(true);
                    index++;
                }
                else
                {
                    break;
                }
            }

            if (index < levelUIs.Count)
            {
                for (int i = index; i < levelUIs.Count; i++)
                {
                    levelUIs[i].gameObject.SetActive(false);
                }
            }
        }

        Color GetColor(int index)
        {
            index %= _colors.Length;
            return _colors[index];
        }

        void ChangeBackground(int index)
        {
            if (_activeBackground != null)
            {
                _activeBackground.SetActive(false);
                if (_activeBackground.GetComponent<ActivateBackgroundLocation>() != null)
                {
                    _activeBackground.GetComponent<ActivateBackgroundLocation>().SetBackgroundAsDisactive();
                }
            }

            if (index < _backgroundsForLocations.Length)
            {
                _activeBackground = _backgroundsForLocations[index];
                if (_activeBackground.GetComponent<ActivateBackgroundLocation>() != null)
                {
                    _activeBackground.GetComponent<ActivateBackgroundLocation>().SetBackgroundAsActive();
                }
                _activeBackground.SetActive(true);
            }

        }
    }
}
