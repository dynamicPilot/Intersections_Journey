using UnityEngine;

public class MapLocationsUI : MonoBehaviour
{
    [SerializeField] private MapLocationUI[] mapLocations;

    [Header("Scripts")]
    [SerializeField] private MenuMaster menuMaster;

    [SerializeField] MapLocationUI locationInButtonsView = null;
    public MapLocationUI LocationInButtonsView { get => locationInButtonsView; set => locationInButtonsView = value; }

    public void SetLocationsInfo(PlayerState playerState)
    {
        locationInButtonsView = null;
        foreach (MapLocationUI locationUI in mapLocations)
        {
            int index = locationUI.GetLocationIndex();
            locationUI.SetLocationState(playerState.LocationsProgress[index].IsAvailable, playerState.LocationsProgress[index].IsPassed,
                playerState.LocationsProgress[index].IsMax);
        }
    }

    public void ResetLocationInButtonState(MapLocationUI caller = null)
    {
        if (caller != null && caller == locationInButtonsView) return;
        if (locationInButtonsView == null) return;

        locationInButtonsView.ResetToViewState();
        locationInButtonsView = null;
    }

    public void OpenLevelsPanelForLocation(Location location)
    {
        menuMaster.OpenLevelsPanelForLocation(location);
    }

    public bool MakeLocationAvalable(Location location)
    {        
        return menuMaster.MakeLocationAvailable(location);
    }

    public bool CanBeLocationAvailable(Location location)
    {
        return menuMaster.CanBeLocationAvailable(location);
    }
}
