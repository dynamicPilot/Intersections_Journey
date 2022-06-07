using UnityEngine;

public class VSortingLayer : MonoBehaviour, IViewSortingLayer
{
    [SerializeField] private SpriteRenderer[] renderers;

    public void SetSortingLayerById(int id)
    {
        if (renderers[0].sortingLayerID == id) return;

        foreach (SpriteRenderer renderer in renderers)
            renderer.sortingLayerID = id;
    }
}
