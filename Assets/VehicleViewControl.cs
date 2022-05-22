using UnityEngine;

public class VehicleViewControl : MonoBehaviour
{
    [Header("Renderers")]
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer leftLightCover;
    [SerializeField] private SpriteRenderer rightLightCover;

    [Header("Packs")]
    [SerializeField] private RendererPackUnit[] packs;

    [Header("Sorting Layers")]
    [SerializeField] string defaultSortingLayer = "Cars";

    public void SetVehicleView(VehicleSpritePack spritePack)
    {
        if (spritePack.body != null && body != null) body.sprite = spritePack.body;
        if (spritePack.leftLightCover != null && leftLightCover != null) leftLightCover.sprite = spritePack.leftLightCover;
        if (spritePack.rightLightCover != null && rightLightCover != null) rightLightCover.sprite = spritePack.rightLightCover;
    }

    public void SetNewSortingLayerByID(int newID)
    {
        if (body != null) body.sortingLayerID = newID;
        if (leftLightCover != null) leftLightCover.sortingLayerID = newID;
        if (rightLightCover != null) rightLightCover.sortingLayerID = newID;

        foreach (RendererPackUnit pack in packs)
        {
            pack.SetRendererSortingLayerByID(newID);
        }
    }

    public void SetDefaultSortingLayer()
    {
        if (body != null) body.sortingLayerID = SortingLayer.NameToID(defaultSortingLayer);
        if (leftLightCover != null) leftLightCover.sortingLayerID = SortingLayer.NameToID(defaultSortingLayer);
        if (rightLightCover != null) rightLightCover.sortingLayerID = SortingLayer.NameToID(defaultSortingLayer);

        SetNewSortingLayerByID(SortingLayer.NameToID(defaultSortingLayer));
    }

    public int GetDefaultSortingLayerID()
    {
        return SortingLayer.NameToID(defaultSortingLayer);
    }
}
