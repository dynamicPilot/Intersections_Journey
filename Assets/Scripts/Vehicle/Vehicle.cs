using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "New Vehicle", menuName = "Unit/Vehicle")]
public class Vehicle : ScriptableObject
{
    [SerializeField] private VehicleUnit.TYPE type;
    public VehicleUnit.TYPE Type { get => type; }

    [SerializeField] private string vehicleName;
    public string VehicleName { get => vehicleName; }
    
    [Header("Sprites")]
    [SerializeField] private VehicleSpritePack[] packs;
    public VehicleSpritePack[] Packs { get => packs; }

    [Header("Parameters")]
    [SerializeField] private float maxVelocity;
    public float MaxVelocity { get => maxVelocity; }

    [SerializeField] private float maxTurningVelocity;
    public float MaxTurningVelocity { get => maxTurningVelocity; }

    [SerializeField] private float maxAcceleration;
    public float MaxAcceleration { get => maxAcceleration; }

    [SerializeField] private float normalAcceleration;
    public float NormalAcceleration { get => normalAcceleration; }

    [SerializeField] private float turningAcceleration;
    public float TurningAcceleration { get => turningAcceleration; }

    [SerializeField] private float mass;
    public float Mass { get => mass; }

    [SerializeField] private float gap;
    public float Gap { get => gap; }

    [Header("Prefab")]
    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get => prefab; }

    //[SerializeField] private AssetReference prefabReference;
    //public AssetReference PrefabReference { get => prefabReference; }

} 
