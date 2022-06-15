using IJ.Core.Objects.Schemes;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.Menus.Main.Levels
{
    public class LevelsPanelUI : MonoBehaviour, ISetPlayerState
    {
        public enum CROSS { tCross, cross, doubleCross, cross3222 }

        [SerializeField] private GameObject[] backgroundsForLocations;
        [SerializeField] private List<LevelUI> levelUIs;

        [Header("Crossroads Schemas")]
        [SerializeField] private CrossroadsSchemes _schemes;

        [Header("Schemas Colors")]
        [SerializeField] private Color[] colors;

        [Header("Components")]
        [SerializeField] private MenuFlow _flow;

        private PlayerState _playerState;
        private GameObject activeBackground = null;
        private SchemesReader _reader;

        private int locationIndex = -1;

        private void Awake()
        {
            locationIndex = -1;
            _reader = new SchemesReader(_schemes);
        }

        Color GetColor(int index)
        {
            index %= colors.Length;
            return colors[index];
        }

        public void SetPlayerState(PlayerState playerState)
        {
            _playerState = playerState;
            locationIndex = -1;
        }

        public void LoadLevel(Level level)
        {
            _flow.LoadLevelScene(level);
        }

        public void OpenLocationLevels(Location location)
        {
            if (locationIndex == location.LocationIndex) return;

            ChangeBackground(location.LocationIndex);
            locationIndex = location.LocationIndex;

            int index = 0;

            for (int i = 0; i < location.Levels.Count; i++)
            {
                if (i < levelUIs.Count)
                {
                    levelUIs[i].SetLevelUI(location.Levels[i], this, _playerState.LevelsProgress[location.Levels[i].LevelIndex], _reader.GetSprite(location.Levels[i].CrossType), GetColor(i));
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

        void ChangeBackground(int index)
        {
            if (activeBackground != null)
            {
                activeBackground.SetActive(false);
                if (activeBackground.GetComponent<ActivateBackgroundLocation>() != null)
                {
                    activeBackground.GetComponent<ActivateBackgroundLocation>().SetBackgroundAsDisactive();
                }
            }

            if (index < backgroundsForLocations.Length)
            {
                activeBackground = backgroundsForLocations[index];
                if (activeBackground.GetComponent<ActivateBackgroundLocation>() != null)
                {
                    activeBackground.GetComponent<ActivateBackgroundLocation>().SetBackgroundAsActive();
                }
                activeBackground.SetActive(true);
            }

        }
    }
}
