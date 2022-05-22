using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimatorControl : MonoBehaviour
{
    [SerializeField] private PlaneAnimator[] planeAnimators;
    [SerializeField] private float updateInterval = 10f;

    WaitForSeconds timer;
    List<int> activePlaneAnimator = new List<int>();
    int counter;

    private void Awake()
    {
        counter = 0;
        timer = new WaitForSeconds(updateInterval);
        StartCoroutine(PlaneSpawner());
    }

    IEnumerator PlaneSpawner()
    {
        yield return timer;

        // first plane
        SpawnNextPlane();
        if (counter < planeAnimators.Length)
        {
            StartCoroutine(PlaneSpawner());
        }
        else
        {
            StartCoroutine(PlaneGetOut());
        }
    }

    IEnumerator PlaneGetOut()
    {
        yield return timer;

        GetOutNextPlane();
        //yield return timer;
        if (activePlaneAnimator.Count > 0) StartCoroutine(PlaneGetOut());
        else
        {
            counter = 0;
            StartCoroutine(PlaneSpawner());
        }
    }

    void SpawnNextPlane()
    {
        if (counter < planeAnimators.Length)
        {
            planeAnimators[counter].gameObject.SetActive(true);
            planeAnimators[counter].StartAnimator();
            activePlaneAnimator.Add(counter);
            counter++;
        }
    }

    void GetOutNextPlane()
    {
        if (activePlaneAnimator.Count > 0)
        {
            int index = activePlaneAnimator[0];           
            planeAnimators[index].MakeGetOut();
            activePlaneAnimator.RemoveAt(0);
        }
    }
}
