using UnityEngine;

public class VehicleTimer : MonoBehaviour
{
    [SerializeField] private RoadMark mark;
    //[SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform markRectTransform;

    float timer = 0f;
    float mainTimerValue = 0f;
    float allertTimerValue = 0f;

    bool isTimerOn = false;
    bool isAllertOn = false;

    private VehicleManager vehicleManager;
    public void SetNewVehicleParameters(EmergencyCar car, VehicleManager newVehicleManager)
    {
        //Logging.Log("VehicleTimer: have an emergency car");
        mark.GetComponent<Canvas>().worldCamera = Camera.main;

        timer = 0f;
        mainTimerValue = car.MainTimer;
        allertTimerValue = car.AllertTimer;

        isTimerOn = false;
        isAllertOn = false;

        vehicleManager = newVehicleManager;
    }

    void CorrectMarkRotationToStayVertical()
    {        
        markRectTransform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, - transform.rotation.z);
        markRectTransform.position = transform.position;
    }

    public void CheckNewVelocity(float newVelocity, float deltaTime)
    {
        //Logging.Log("VehicleTimer: check timer " + newVelocity);
        if (isTimerOn)
        {
            if (newVelocity > 0)
            {
                OnVehicleStart();
            }
            else
            {
                UpdateTimer(deltaTime);
            }
        }
        else if (newVelocity == 0)
        {
            OnVehicleStop();
        }
    }
    
    void UpdateTimer(float deltaTime)
    {
        timer += deltaTime;
        CorrectMarkRotationToStayVertical();

        if (timer >= mainTimerValue)
        {
            if (!isAllertOn)
            {
                // start allert
                mark.MoveToAllert();
                isAllertOn = true;
            }
            else if (timer >= mainTimerValue + allertTimerValue)
            {
                // game over
                vehicleManager.GameOverForEmergencyCar(transform.position);
            }
            else
            {
                mark.UpdateIndicatorValue((timer - mainTimerValue) / allertTimerValue, true);
            }
        }
        else
        {
            mark.UpdateIndicatorValue(timer/ mainTimerValue, false);
        }
    }

    void OnVehicleStop()
    {
        Logging.Log("VehicleTimer: start timer");
        isTimerOn = true;
        // start timer
        if (mark.CheckForStartIndicator())
        {
            mark.gameObject.SetActive(true);
            CorrectMarkRotationToStayVertical();
            mark.StartIndicator();
        }
    }

    void OnVehicleStart()
    {
        // stop indicator
        isTimerOn = false;
        if (mark.gameObject.activeSelf)
        {
            mark.StopIndicator();
        }
        
    }

    public void HideMarkAndTimer()
    {
        OnVehicleStart();
        timer = 0f;
    }

    private void OnDisable()
    {
        //OnVehicleStart();
        mark.gameObject.SetActive(false);
        timer = 0f;
    }
}
