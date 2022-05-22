using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairSitesControl : MonoBehaviour
{
    [SerializeField] private List<RepairSite> repairSites;
    [SerializeField] private AnimatedButton needRepairCarButton;
    [SerializeField] private float timeToRepair = 2f;
    public float TimeToRepair { get => timeToRepair; }

    [Header("Scripts")]
    [SerializeField] private VehicleCreator vehicleCreator;

    List<TimePoint> timePoints = new List<TimePoint>();
    [SerializeField] List<int> sitesToBeRepaired = new List<int>();
    [SerializeField] List<int> sitesWaitingForCar = new List<int>();

    IDictionary<TimePoint, int> repairSiteIndexByTimePoints = new Dictionary<TimePoint, int>();

    [SerializeField] bool needRepairSiteControl = false;

    private void Awake()
    {
        needRepairSiteControl = false;

        timePoints.Clear();
        repairSiteIndexByTimePoints.Clear();
        sitesToBeRepaired.Clear();
        sitesWaitingForCar.Clear();

        for(int i = 0; i < repairSites.Count; i++)
        {
            repairSites[i].RepairSiteIndex = i;
        }
    }

    public void SetRepairSitesTimePoints(List<TimePoint> newTimePoints)
    {
        needRepairSiteControl = false;
        if (newTimePoints != null)
        {
            if (newTimePoints.Count > 0)
            {
                timePoints = newTimePoints;
                needRepairSiteControl = true;
            }
        }

        FillRepairSiteIndexByTimePoints();

    }

    void FillRepairSiteIndexByTimePoints()
    {
        if (!needRepairSiteControl) return;

        repairSiteIndexByTimePoints.Clear();

        // set dictionary
        foreach (TimePoint point in timePoints)
        {
            repairSiteIndexByTimePoints[point] = -1;
            // find repair site
            for (int i = 0; i < repairSites.Count; i++)
            {
                if (repairSites[i].IsTheSameLocation(point.RoadStartPointNumber, point.RoadEndPointNumber) && point.IsDouble == repairSites[i].IsDouble)
                {
                    repairSiteIndexByTimePoints[point] = i;
                    break;
                }
            }
        }
    }

    public void CheckRepairSite(int hour)
    {
        if (!needRepairSiteControl) return;

        foreach (TimePoint point in timePoints)
        {
            if (point.Hour == hour)
            {
                // activate repair site
                if (repairSiteIndexByTimePoints.ContainsKey(point) && repairSiteIndexByTimePoints[point] != -1)
                {
                    ActivateRepairSite(repairSiteIndexByTimePoints[point]);
                    repairSiteIndexByTimePoints.Remove(point);
                }
            }
        }

        if (repairSiteIndexByTimePoints.Count == 0) needRepairSiteControl = false;
    }

    void ActivateRepairSite(int index)
    {
        if (sitesToBeRepaired.Contains(index) || sitesWaitingForCar.Contains(index))
        {
            return;
        }

        // activate repair site
        repairSites[index].gameObject.SetActive(true);
        repairSites[index].ActivateSite();

        sitesToBeRepaired.Add(index);

        // show UI
        if (!needRepairCarButton.gameObject.activeSelf)
        {
            needRepairCarButton.StartButton();
            needRepairCarButton.gameObject.SetActive(true);           
        }
        else
        {
            needRepairCarButton.CheckForStartIndicator();
        }
    }

    public void HideRepairSite(int index)
    {
        if (!sitesWaitingForCar.Contains(index)) return;

        Logging.Log("RepairSideControl: hide side");
        sitesWaitingForCar.Remove(index);
        repairSites[index].StopSite();
        repairSites[index].gameObject.SetActive(false);       
    }

    public void CallForRepairCar()
    {
        sitesWaitingForCar.Add(sitesToBeRepaired[0]);
        sitesToBeRepaired.RemoveAt(0);

        // set call to Vehicle Creator
        int index = sitesWaitingForCar[sitesWaitingForCar.Count - 1];
        vehicleCreator.CallForRepairCar(repairSites[index].Location, index, this);

        // if do not have another active sites
        if (sitesToBeRepaired.Count == 0)
        {
            needRepairCarButton.StopButton();
        }
        //needRepairCarButton.gameObject.SetActive(sitesToBeRepaired.Count != 0);
    }

    //public List<int> GetRoadPointsAndIndexToRepairCar()
    //{

    //}
}
