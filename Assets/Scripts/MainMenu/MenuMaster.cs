using UnityEngine;


public class MenuMaster : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private MapLocationsUI mapLocationsUI;
    [SerializeField] private LevelsPanelUI levelsPanelUI;
    [SerializeField] private LocationOpeningUI openingUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PointsControl pointsControl;

    [SerializeField] PlayerState playerState;

    public void SetMenu(PlayerState newPlayerState)
    {
        playerState = newPlayerState;

        if (levelsPanelUI.gameObject.activeSelf) levelsPanelUI.gameObject.SetActive(false);

        // set points control
        pointsControl.SetPointsControl(playerState);

        // set locations' info
        mapLocationsUI.SetLocationsInfo(playerState);
        mapLocationsUI.gameObject.SetActive(true);
    }

    public void CloseStuffOnClick()
    {
        mapLocationsUI.ResetLocationInButtonState();
    }

    public void OpenLevelsPanelForLocation(Location location)
    {
        if (!playerState.LocationsProgress[location.LocationIndex].IsAvailable)
        {
            // location is not available
            Logging.Log("MenuMaster: location is not available " + location.LocationName);
            return;
        }

        mapLocationsUI.gameObject.SetActive(false);
        levelsPanelUI.gameObject.SetActive(true);
        levelsPanelUI.SetLocation(location, playerState);
    }

    public bool MakeLocationAvailable(Location location)
    {
        // check if can
        if (pointsControl.HaveSomePoints(location.PointsToMakeAvailable))
        {
            // make available
            playerState.MakeLocationAvailableOrNot(location.LocationIndex, true);

            // spend points
            pointsControl.ChangePointsAmount(-location.PointsToMakeAvailable, true);

            // start opening effect
            openingUI.OpenLocationByIndex(location.LocationIndex);

            return true;
        }
        return false;
    }

    public bool CanBeLocationAvailable(Location location)
    {
        if (!pointsControl.HaveSomePoints(location.PointsToMakeAvailable)) return false;

        Logging.Log("MenuMaster: " + location.LocationName);
        if (location.LocationsToMakeAvailable == null)
        {
            Logging.Log("MenuMaster: location list is null " + location.LocationName);
            return true;
        }
        // check locations to be available
        foreach (Location loc in location.LocationsToMakeAvailable)
        {
            if (!playerState.IsLocationAvailable(loc.LocationIndex)) return false;
        }

        return true;
    }

    public void LoadLevel(Level level)
    {
        levelsPanelUI.gameObject.SetActive(false);

        // load level
        gameManager.LoadLevel(level);
    }

    public void BackToMenu()
    {
        levelsPanelUI.gameObject.SetActive(false);
        mapLocationsUI.gameObject.SetActive(true);
    }


}
