using IJ.Core.UIElements.NeedRepairCar;
using System.Collections.Generic;
using UnityEngine;

namespace RepairSites
{
    public class RepairSitesControl : MonoBehaviour
    {
        [SerializeField] private RepairSite[] repairSites;
        [SerializeField] private NeedRepairCarButtonEffects _needRepairCarButton;
        bool _needRepairSiteControl = false;

        public delegate void NeedRepairCar(TimePoint timePoint, int repairSiteIndex);
        public event NeedRepairCar OnNeedRepairCar;

        private RepairSiteData data;
        private List<VRepairSiteTagForRepairCar> subscriptions = new List<VRepairSiteTagForRepairCar>();

        private void Awake()
        {
            _needRepairSiteControl = false;

            for (int i = 0; i < repairSites.Length; i++)
            {
                repairSites[i].RepairSiteIndex = i;
            }
        }

        private void OnDestroy()
        {
            foreach (VRepairSiteTagForRepairCar tag in subscriptions)
            {
                tag.OnRepairIsMade -= HideRepairSite;
            }
        }

        public void SubscribeUnitInfo(VRepairSiteTagForRepairCar tag)
        {
            tag.OnRepairIsMade += HideRepairSite;
            subscriptions.Add(tag);
        }

        public void UnsubscribeUnitInfo(VRepairSiteTagForRepairCar tag)
        {
            tag.OnRepairIsMade -= HideRepairSite;
            subscriptions.Remove(tag);
        }

        public void SetRepairSitesTimePoints(List<TimePoint> timePoints)
        {
            if (timePoints == null || timePoints.Count == 0)
            {
                Logging.Log("RepairSiteControl: no repair site!");
                _needRepairSiteControl = false;
                return;
            }

            Logging.Log("RepairSiteControl: have repair site!");
            data = new RepairSiteData(timePoints, repairSites);
            _needRepairSiteControl = true;
        }

        public void CheckRepairSite(int hour)
        {
            if (!_needRepairSiteControl) return;

            ActivateRepairSite(data.CheckRepairSiteForMakeActive(hour, out _needRepairSiteControl));

        }

        void ActivateRepairSite(int index)
        {
            if (index < 0) return;

            // activate repair site
            repairSites[index].gameObject.SetActive(true);
            repairSites[index].ActivateSite();

            // show UI
            if (!_needRepairCarButton.gameObject.activeSelf)
            {
                _needRepairCarButton.StartButton();
            }
        }

        public void HideRepairSite(int index, VRepairSiteTagForRepairCar tag)
        {
            if (!data.CheckRepairSiteToHide(index)) return;

            repairSites[index].StopSite();
            repairSites[index].gameObject.SetActive(false);
            tag.OnRepairIsMade -= HideRepairSite;
        }

        public void CallForRepairCar()
        {
            //vehicleCreator.CallForRepairCar(repairSites[index].Location, index, this);
            int index = 0;
            bool hasMoreActive = data.OnCallForRepairCar(out index);

            if (OnNeedRepairCar != null) OnNeedRepairCar.Invoke(repairSites[index].Location, index);

            _needRepairCarButton.StopButton();

            if (hasMoreActive)
            {
                _needRepairCarButton.StartButton();
            }
        }
    }
}

