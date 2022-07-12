using IJ.Core.Menus.Main.Levels;
using IJ.Core.Menus.Others;
using UnityEngine;

namespace IJ.Core.Menus.MainMenu
{
    public class PanelsControl : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private MapLocationsUI _map;
        [SerializeField] private LevelsPanelUI _levels;
        [SerializeField] private LocationOpeningUI _opening;
        [SerializeField] private SettingsPanelUI _settings;
        [SerializeField] private MovablePanelUI _credits;

        public void OpenMap()
        {
            _levels.HidePanel();
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
            _levels.OpenLocationPage(location);
        }

        public void BackToMenu()
        {
            _map.gameObject.SetActive(true);
            _levels.ClosePage();
            _settings.ClosePage();
            _credits.ClosePage();
        }

        public void TransitToLevelScene()
        {
            //_levels.gameObject.SetActive(false);
        }
    }
}
