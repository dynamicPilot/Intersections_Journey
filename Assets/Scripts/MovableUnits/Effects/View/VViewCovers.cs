using UnityEngine;

public class VViewCovers : VView
{
    [SerializeField] private SpriteRenderer leftLightCover;
    [SerializeField] private SpriteRenderer rightLightCover;

    public override void SetView()
    {
        int index = Random.Range(0, packs.Length);
        packs[index].SetSprites(new SpriteRenderer[3] { body, leftLightCover, rightLightCover });
    }

    public override void SetSortingLayerById(int id)
    {
        base.SetSortingLayerById(id);

        if (leftLightCover.sortingLayerID == id) return;
        leftLightCover.sortingLayerID = id;
        rightLightCover.sortingLayerID = id;
    }
}
