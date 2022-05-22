using UnityEngine;

public class MapLocationsUI : MonoBehaviour
{
    [SerializeField] private MapLocationUI[] mapLocations;

    [Header("Scripts")]
    [SerializeField] private MenuMaster menuMaster;

    public void SetLocationsInfo(PlayerState playerState)
    {
        foreach (MapLocationUI locationUI in mapLocations)
        {
            int index = locationUI.GetLocationIndex();
            locationUI.SetLocationState(playerState.LocationsProgress[index].IsAvailable, playerState.LocationsProgress[index].IsPassed,
                playerState.LocationsProgress[index].IsMax);
        }
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
