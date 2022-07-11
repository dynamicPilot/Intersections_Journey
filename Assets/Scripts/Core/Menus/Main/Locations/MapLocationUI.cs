using IJ.Animations;
using IJ.UIElements;
using System.Collections;
using TMPro;
using UnityEngine;

public class MapLocationUI : MonoBehaviour
{
    [SerializeField] private Location location;
    [SerializeField] private MapLocationsUI mapLocationsUI;
    [SerializeField] private PanelsFlipAnimation _animation;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private DisableButtonWithIcon _openLocationButton;

    [Header("Views")]
    [SerializeField] private GameObject closedView;
    [SerializeField] private GameObject openedView;
    [SerializeField] private GameObject decorations;

    WaitForSeconds timer;
    [SerializeField] bool isBlock = false;
    [SerializeField] bool inView = true;
    [SerializeField] bool isAvailable = false;

    public void SetLocationState(bool newIsAvailable, bool isMax)
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

        if (_animation != null)
        {
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
        }
    }

    public void OnLocationClick()
    {
        mapLocationsUI.ResetLocationInButtonState(this);
        if (isAvailable && !isBlock)
        {
            mapLocationsUI.OpenLevelsPanelForLocation(location);
        }
        else
        {
            // open button panel           

            if (_animation != null)
            {            
                StartCoroutine(IsBlock());
            }
        }
    }

    IEnumerator IsBlock()
    {
        isBlock = true;
        if (_openLocationButton != null) _openLocationButton.Interactive(mapLocationsUI.CanBeLocationAvailable(location));

        if (inView) mapLocationsUI.LocationInButtonsView = this;
        else mapLocationsUI.LocationInButtonsView = null;

        _animation.DoFlip();
        yield return timer;

        isBlock = false;
        inView = !inView;
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
            mapLocationsUI.LocationInButtonsView = null;
            SetLocationView();
        }
    }
}
