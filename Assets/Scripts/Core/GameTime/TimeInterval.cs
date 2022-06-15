using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TimeIntervalsByRoad
{
    [SerializeField] private int roadStartPointNumber;
    public int RoadStartPointNumber { get => roadStartPointNumber; }

    [SerializeField] private List<TimeInterval> timeIntervals;
    public List<TimeInterval> TimeIntervals { get => timeIntervals; }
}

[System.Serializable]
public struct TimeInterval
{
    [SerializeField] private int startHour;
    public int StartHour { get => startHour; }

    [SerializeField] private int endHour;
    public int EndHour { get => endHour; }

    [SerializeField] List<VehicleNumberByType> vehicleNumbersList;
    public List<VehicleNumberByType> VehicleNumbersList { get => vehicleNumbersList; }
}

[System.Serializable]
public struct VehicleNumberByType
{
    [SerializeField] public VehicleUnit.TYPE type;
    [SerializeField] public TYPE _type;
    [SerializeField] public int number;
    [SerializeField] public bool needSpecialEndPoint;
    //public int startPointNumber;
    [SerializeField] public int endPointNumber;


    public VehicleNumberByType(TYPE newType, int newNumber, bool newNeedSpecialEndPoint = false, int newEndPoint = -1)
    {
        type = (VehicleUnit.TYPE) ((int)newType);
        _type = newType;
        number = newNumber;
        //startPointNumber = -1;
        needSpecialEndPoint = newNeedSpecialEndPoint;
        endPointNumber = (needSpecialEndPoint) ? newEndPoint : -1;  
    }
}
