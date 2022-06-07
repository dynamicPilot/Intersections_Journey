using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour, IViewSortingLayer
{
    [SerializeField] private SpriteRenderer[] cargos;
    [SerializeField] private Color[] colors;

    [Header("Points")]
    [SerializeField] private float[] xCoord = { -0.41f, 0.0f, 0.41f };
    [SerializeField] private float[] yCoord = { -0.46f, -1.05f };
    
    List<Vector2> points;
    List<int> activeCargoIndexes = new List<int>();
    List<int> activePointIndexes = new List<int>();


    private void Awake()
    {
        // generate points
        points = new List<Vector2>();

        foreach(float x in xCoord)
        {
            foreach(float y in yCoord)
            {
                points.Add(new Vector2 (x, y));
            }
        }

        SetCargoState(true);
    }

    public void SetSortingLayerById(int newID)
    {
        if (activeCargoIndexes == null && activeCargoIndexes.Count == 0) return;

        if (activeCargoIndexes.Count > 1 && cargos[0].sortingLayerID == newID) return;

        foreach (int index in activeCargoIndexes)
        {
            cargos[index].sortingLayerID = newID;
        }
    }

    void SetCargoState(bool mode = true)
    {
        HideCargo();

        if (mode)
        {
            ShowCargo();
        }

    }

    void HideCargo()
    {
        foreach (SpriteRenderer cargo in cargos)
        {
            if (cargo.gameObject.activeSelf) cargo.gameObject.SetActive(false);
        }
        activeCargoIndexes.Clear();
        activePointIndexes.Clear();
    }

    void ShowCargo()
    {
        int numberToActivate = Random.Range(1, cargos.Length+1);
        for (int i = 0; i < numberToActivate; i++) ActivateRandomCargo();

    }

    void ActivateRandomCargo()
    {
        int currentCargoIndex = Random.Range(0, cargos.Length);

        while(activeCargoIndexes.Contains(currentCargoIndex)) currentCargoIndex = Random.Range(0, cargos.Length);

        int currentPointIndex = Random.Range(0, points.Count);

        while (activePointIndexes.Contains(currentPointIndex)) currentPointIndex = Random.Range(0, points.Count);

        // activate cargo
        cargos[currentCargoIndex].color = colors[Random.Range(0, colors.Length)];
        //cargos[currentCargoIndex].GetComponent<Transform>().position = new Vector3(points[currentCargoIndex].x, points[currentCargoIndex].y, 0f);
        cargos[currentCargoIndex].gameObject.SetActive(true);
        activeCargoIndexes.Add(currentCargoIndex);
        activePointIndexes.Add(currentPointIndex);

    }
}
