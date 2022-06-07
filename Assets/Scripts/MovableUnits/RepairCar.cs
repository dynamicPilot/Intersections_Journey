using System.Collections;
using UnityEngine;

public class RepairCar : MonoBehaviour
{
    int repairSiteIndex = 0;
    public int RepairSiteIndex { get => repairSiteIndex;}

    bool repairIsMade = false;
    public bool RepairIsMade { get => repairIsMade; }

    bool isInRepair = false;

    //private RepairSitesControl repairSitesControl;

    public void SetRepairCar(int index)
    {
        repairSiteIndex = index;
        //repairSitesControl = newRepairSitesControl;

        repairIsMade = false;
        isInRepair = false;
    }

    public void GoToTargetRepairSite()
    {
        if (isInRepair)
        {
            return;
        }
        //else if (repairSitesControl == null)
        //{
        //    LeaveRepairSite();
        //}
        else
        {
            StartCoroutine(RepairingTimer());
        }
    }

    IEnumerator RepairingTimer()
    {
        Logging.Log("RepairCar: start repairing");
        isInRepair = true;
        yield return new WaitForSeconds(1f); //repairSitesControl.TimeToRepair

        // hide repair side
        //repairSitesControl.HideRepairSite(repairSiteIndex);
        repairIsMade = true;
        isInRepair = false;
        LeaveRepairSite();
    }

    void LeaveRepairSite()
    {
        Logging.Log("RepairCar: leaving repair site");
        //GetComponent<VehicleUnit>().ExitRepairSite(null);
    }

    public void StopVehicle()
    {
        StopAllCoroutines();
        repairIsMade = false;
        isInRepair = false;
    }
}
