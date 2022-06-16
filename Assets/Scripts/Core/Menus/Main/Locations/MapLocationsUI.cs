using IJ.Core.Menus.MainMenu;
using UnityEngine;

public class MapLocationsUI : MonoBehaviour, ISetPlayerState
{
    [SerializeField] private MapLocationUI[] mapLocations;

    [Header("Scripts")]
    [SerializeField] private MenuMaster menuMaster;

    MapLocationUI locationInButtonsView = null;
    public MapLocationUI LocationInButtonsView { get => locationInButtonsView; set => locationInButtonsView = value; }

    public void SetPlayerState(PlayerState playerState)
    {
        SetMap(playerState);
    }

    void SetMap(PlayerState playerState)
    {
        locationInButtonsView = null;

        foreach (MapLocationUI locationUI in mapLocations)
        {
            int index = locationUI.GetLocationIndex();
            locationUI.SetLocationState(playerState.LocationsProgress[index].IsAvailable,playerState.LocationsProgress[index].IsMax);
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
