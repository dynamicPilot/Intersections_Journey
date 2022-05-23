using UnityEngine;

[CreateAssetMenu(fileName = "New EmergencyCar", menuName = "Unit/EmergencyCar")]
public class EmergencyCar : Vehicle
{
    [Header("Emergency Car Settings")]
    [SerializeField] private float mainTimer;
    public float MainTimer { get => mainTimer; }

    [SerializeField] private float allertTimer;
    public float AllertTimer { get => allertTimer; }
}
