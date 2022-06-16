using IJ.Core.Menus.Main.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.Menus.MainMenu
{
    public class PanelsControl : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private MapLocationsUI _map;
        [SerializeField] private LevelsPanelUI _levels;
        [SerializeField] private LocationOpeningUI _opening;

        public void OpenMap()
        {
            if (_levels.gameObject.activeSelf) _levels.gameObject.SetActive(false);
            _map.gameObject.SetActive(true);
        }

        public void TransitToOpening(int locationIndex)
        {
            _opening.OpenLocationByIndex(locationIndex);
        }

        public void FlipInvertedLocationOver()
        {
            _map.ResetLocationInButtonState();
        }

        public void TransitToLevels(Location location)
        {
            _map.gameObject.SetActive(false);
            _levels.gameObject.SetActive(true);
            _levels.OpenLocationPage(location);
        }

        public void BackToMenu()
        {
            _map.gameObject.SetActive(true);
            _levels.CloseLocationPage();           
        }

        public void TransitToLevelScene()
        {
            _levels.gameObject.SetActive(false);
        }
    }
}
