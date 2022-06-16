using IJ.Core.Objects.LevelAndLocation;
using UnityEngine;
using UnityEngine.UI;

namespace IJ.Core.Menus.Main.Levels
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private StarsControl starsControl;
        [SerializeField] private Image crossroadsSchema;
        [SerializeField] private Button button;

        private Level _level;
        private LevelsPanelUI _levelsPanelUI;

        public void SetLevelUI(Level level, LevelsPanelUI levelPanelUI, LocationOrLevelProgress levelProgress, Sprite schema, Color color)
        {
            _level = level;
            _levelsPanelUI = levelPanelUI;

            button.interactable = levelProgress.IsAvailable;

            // set stars
            starsControl.SetStars(levelProgress.PointsEarned, levelProgress.MaxPoints);

            // set crossroads schema
            crossroadsSchema.sprite = schema;
            crossroadsSchema.color = color;
        }

        public void OnLevelClick()
        {
            _levelsPanelUI.LoadLevel(_level);
        }
    }
}
