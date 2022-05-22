using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private StarsControl starsControl;
    [SerializeField] private Image crossroadsSchema;
    [SerializeField] private Button button;

    private Level level;
    private LevelsPanelUI levelsPanelUI;

    public void SetLevelUI(Level newLevel, LevelsPanelUI newLevelPanelUI, LocationOrLevelProgress levelProgress, Sprite schema, Color color)
    {
        Logging.Log("LevelUI: set for level " + newLevel.Number + " level is available " + levelProgress.IsAvailable);
        level = newLevel;
        levelsPanelUI = newLevelPanelUI;

        button.interactable = levelProgress.IsAvailable;

        // set stars
        starsControl.SetStars(levelProgress.PointsEarned, levelProgress.MaxPoints);

        // set crossroads schema
        crossroadsSchema.sprite = schema;
        crossroadsSchema.color = color;
    }

    public void OnLevelClick()
    {
        levelsPanelUI.LoadLevel(level);
    }
}
