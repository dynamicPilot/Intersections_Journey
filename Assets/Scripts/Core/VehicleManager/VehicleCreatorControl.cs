using System.Collections.Generic;
using UnityEngine;

public class VehicleCreatorControl : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private TimeControl timeControl;
    [SerializeField] private VehicleCreator vehicleCreator;

    IDictionary<int, IDictionary<int, List<VehicleNumberByType>>> timetable = new Dictionary<int, IDictionary<int, List<VehicleNumberByType>>>();

    List<int> minuteIntervals = new List<int>();

    public void CheckTime(int hour, int minutes)
    {
        if (!timetable.ContainsKey(hour)) return;

        if (timetable[hour][minutes].Count > 0)
        {
            foreach(VehicleNumberByType numberByType in timetable[hour][minutes])
            {
                vehicleCreator.CreateVehicle(numberByType);
            }
        }
    }


    public void SetVehicleTimetable(TimeIntervalsByRoad[] timeIntervalsByRoads)
    {
        minuteIntervals = timeControl.MinuteIntervals;

        for (int i = 0; i < timeControl.EndHour; i++)
        {
            timetable[i] = new Dictionary<int, List<VehicleNumberByType>>();
            
            // set minutes intervals
            foreach (int minute in minuteIntervals)
            {
                timetable[i][minute] = new List<VehicleNumberByType>();
            }
        }

        // read for each start point number
        foreach (TimeIntervalsByRoad timeIntervalsByRoad in timeIntervalsByRoads)
        {
            // get clear timetable
            IDictionary<int, List<bool>> timetableForStartPoints = GetNewTimetable();

            // read all time intervals
            foreach (TimeInterval timeInterval in timeIntervalsByRoad.TimeIntervals)
            {
                // read all vehicle type and according numbers
                foreach (VehicleNumberByType vehicleNumber in timeInterval.VehicleNumbersList)
                {
                    for (int i = 0; i < vehicleNumber.number; i++)
                    {
                        // read start and end hour of the interval
                        int hour = GetRandomHourFromInterval(timeInterval.StartHour, timeInterval.EndHour, timetableForStartPoints);

                        if (hour == -1) break;

                        int minuteIndex = GetRandomMinuteIndexFromHourInterval(hour, timetableForStartPoints);

                        if (minuteIndex == -1) break;

                        // set this vehicle in timetable
                        timetableForStartPoints[hour][minuteIndex] = false;
                        timetable[hour][minuteIntervals[minuteIndex]].Add(new VehicleNumberByType((TYPE)((int)vehicleNumber.type), timeIntervalsByRoad.RoadStartPointNumber, vehicleNumber.needSpecialEndPoint, vehicleNumber.endPointNumber));
                    }
                }
            }
        }
    }

    IDictionary<int, List<bool>> GetNewTimetable()
    {
        IDictionary<int, List<bool>> temp = new Dictionary<int, List<bool>>();

        foreach(int hour in timetable.Keys)
        {
            temp[hour] = new List<bool>();

            foreach (int minute in minuteIntervals)
            {
                temp[hour].Add(true);
            }
        }

        return temp;
    }

    int GetRandomHourFromInterval(int startHour, int endHour, IDictionary<int, List<bool>> timetableForStartPoints)
    {
        List<int> hoursToChooseFrom = new List<int>();

        for (int i = startHour; i < endHour; i++) hoursToChooseFrom.Add(i);

        while (hoursToChooseFrom.Count > 0)
        {
            // choose random number
            int randomHour = hoursToChooseFrom[Random.Range(0, hoursToChooseFrom.Count)];

            if (timetableForStartPoints[randomHour].Contains(true))
            {
                // find suitable hour
                return randomHour;
            }
            else
            {
                hoursToChooseFrom.Remove(randomHour);
            }
        }

        return -1;
    }

    int GetRandomMinuteIndexFromHourInterval(int hour, IDictionary<int, List<bool>> timetableForStartPoints)
    {
        if (!timetableForStartPoints[hour].Contains(true)) return -1;

        while (true)
        {
            int randomMinuteIndex = Random.Range(0, timetableForStartPoints[hour].Count);

            if (timetableForStartPoints[hour][randomMinuteIndex])
            {
                return randomMinuteIndex;
            }
        }
    }

    void ShowTimetable()
    {
        //Logging.Log("VehicleCreatorControl: SHOW TIMETABLE");

        foreach(int hour in timetable.Keys)
        {
            //Logging.Log("HOUR " + hour);

            foreach (int minute in timetable[hour].Keys)
            {
                if (timetable[hour][minute].Count > 0)
                {
                    foreach(VehicleNumberByType numberByType in timetable[hour][minute])
                    {
                        //Logging.Log("MINUTE " + minute + " vehicle type " + numberByType.type + " for start point " + numberByType.number);
                    }
                }
            }
        }
    }


}
