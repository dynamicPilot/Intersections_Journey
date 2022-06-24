using System.Collections.Generic;
using UnityEngine;

namespace RepairSites
{
    [System.Serializable]
    public class RepairSiteData
    {
        [SerializeField] List<TimePoint> _timePoints = new List<TimePoint>();
        [SerializeField] List<int> _sitesToBeRepaired = new List<int>();
        [SerializeField] List<int> _sitesWaitingForCar = new List<int>();

        IDictionary<TimePoint, int> _repairSiteIndexByTimePoints = new Dictionary<TimePoint, int>();

        public RepairSiteData(List<TimePoint> timePoints, RepairSite[] repairSites)
        {
            _timePoints = timePoints;
            FillRepairSiteIndexByTimePoints(repairSites);
        }

        void FillRepairSiteIndexByTimePoints(RepairSite[] repairSites)
        {
            // set dictionary
            foreach (TimePoint point in _timePoints)
            {
                _repairSiteIndexByTimePoints[point] = -1;
                // find repair site
                for (int i = 0; i < repairSites.Length; i++)
                {
                    if (repairSites[i].IsTheSameLocation(point.RoadStartPointNumber, point.RoadEndPointNumber) && point.IsDouble == repairSites[i].IsDouble)
                    {
                        _repairSiteIndexByTimePoints[point] = i;
                        break;
                    }
                }
            }
        }

        public int CheckRepairSiteForMakeActive(int hour, out bool continueControl)
        {
            Logging.Log("RepairSiteData: check for active site");
            int index = -1;

            foreach (TimePoint point in _timePoints)
            {
                if (point.Hour == hour)
                {
                    // activate repair site
                    if (_repairSiteIndexByTimePoints.ContainsKey(point) && _repairSiteIndexByTimePoints[point] != -1)
                    {
                        index = _repairSiteIndexByTimePoints[point];

                        if (_sitesToBeRepaired.Contains(index) || _sitesWaitingForCar.Contains(index))
                        {
                            index = -1;
                            continue;
                        }

                        _repairSiteIndexByTimePoints.Remove(point);
                        _sitesToBeRepaired.Add(index);
                    }
                }
            }

            continueControl = (_repairSiteIndexByTimePoints.Count != 0);
            return index;
        }

        public bool CheckRepairSiteToHide(int index)
        {
            if (!_sitesWaitingForCar.Contains(index)) return false;

            _sitesWaitingForCar.Remove(index);
            return true;
        }

        public bool OnCallForRepairCar(out int index)
        {
            _sitesWaitingForCar.Add(_sitesToBeRepaired[0]);
            _sitesToBeRepaired.RemoveAt(0);

            index = _sitesWaitingForCar[^1];

            return (_sitesToBeRepaired.Count != 0);
   
        }
    }
}

