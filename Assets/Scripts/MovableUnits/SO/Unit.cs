using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit/Unit")]
public class Unit : ScriptableObject
{
    [SerializeField] private TYPE type;
    public TYPE Type { get => type; }

    [SerializeField] private string vehicleName;
    public string VehicleName { get => vehicleName; }

    [Header("Prefab")]
    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get => prefab; }
}
