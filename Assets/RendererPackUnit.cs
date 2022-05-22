using UnityEngine;

public class RendererPackUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] renderers;

    public void SetColor(Color newColor)
    {

    }

    // set order layer
    public void SetRendererSortingLayerByID(int newID)
    {
        if (renderers.Length > 0)
        {
            if (renderers[0].sortingLayerID == newID) return;
        }

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingLayerID = newID;
        }
    }
}
