using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapLocationUI : MonoBehaviour
{
    [SerializeField] private Location location;
    [SerializeField] private MapLocationsUI mapLocationsUI;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Button openLocationButton;
    [SerializeField] private Animator animator;

    [Header("Views")]
    [SerializeField] private GameObject closedView;
    [SerializeField] private GameObject openedView;
    [SerializeField] private GameObject decorations;

    WaitForSeconds timer;
    [SerializeField] bool isBlock = false;
    [SerializeField] bool inView = true;
    [SerializeField] bool isAvailable = false;

    public void SetLocationState(bool newIsAvailable, bool isPassed, bool isMax)
    {
        // set state
        isAvailable = newIsAvailable;
        SetLocationView();

        inView = true;
        isBlock = false;
        timer = new WaitForSeconds(1f);
    }

    public void ResetToViewState()
    {
        if (inView) return;

        //StopAllCoroutines();
        if (animator != null)
        {
            Logging.Log("MapLocationUI: start animator");
            StartCoroutine(IsBlock());
        }
    }

    void SetLocationView()
    {
        if (isAvailable)
        {
            //animator.enabled = false;
            if (closedView != null) closedView.SetActive(false);

            if (openedView != null) openedView.SetActive(true);
            if (decorations != null) decorations.SetActive(true);
        }
        else
        {
            if (openedView != null) openedView.SetActive(false);
            if (decorations != null) decorations.SetActive(false);

            if (closedView != null) closedView.SetActive(true);

            if (pointsText != null) pointsText.SetText(location.PointsToMakeAvailable.ToString());
            if (animator != null) animator.enabled = true;
        }
    }

    public void OnLocationClick()
    {
        mapLocationsUI.ResetLocationInButtonState(this);
        if (isAvailable && !isBlock)
        {
            Logging.Log("MapLocationUI: opening level panel");
            mapLocationsUI.OpenLevelsPanelForLocation(location);
        }
        else
        {
            // open button panel           

            if (animator != null)
            {
                Logging.Log("MapLocationUI: start animator");                
                StartCoroutine(IsBlock());
            }
        }
    }

    IEnumerator IsBlock()
    {
        //Logging.Log("MapLocationUI: start isBlock go to view " + !inView);
        isBlock = true;

        if (inView)
        {
            //Logging.Log("MapLocationUI: to buttons ");
            animator.SetTrigger("ToButtons");
            mapLocationsUI.LocationInButtonsView = this;
        }
        else
        {
            //Logging.Log("MapLocationUI: to view ");
            animator.SetTrigger("ToView");
            mapLocationsUI.LocationInButtonsView = null;
        }

        yield return timer;

        isBlock = false;
        inView = !inView;

        if (!inView)
        {
            // set button state
            if (openLocationButton != null) openLocationButton.interactable = mapLocationsUI.CanBeLocationAvailable(location);
        }
       
        //Logging.Log("MapLocationUI: end isBlock");
    }

    public int GetLocationIndex()
    {
        return location.LocationIndex;
    }

    public void MakeLocationAvailable()
    {
        isAvailable = mapLocationsUI.MakeLocationAvalable(location);

        if (isAvailable)
        {
            // start opening effect is made by Menu Master so just update view
            mapLocationsUI.LocationInButtonsView = null;
            SetLocationView();
        }
    }
}
