using UnityEngine;

public class ParkingManager : MonoBehaviour
{
    [Header("Parkings")]
    [SerializeField] private Parking[] parkings;

    public void SetActiveParkings(Level level)
    {        
        if (parkings == null) return;
        else if (parkings.Length < 1) return;

        // set active parkings by number
    }
}
