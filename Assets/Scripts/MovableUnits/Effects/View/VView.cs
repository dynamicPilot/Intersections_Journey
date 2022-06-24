using UnityEngine;
using Utilites.SpritePacks;

public class VView : MonoBehaviour, IViewSortingLayer
{
    [Header("Sprite")]
    [SerializeField] protected private VSpritePack[] packs;

    [Header("Renderers")]
    [SerializeField] protected private SpriteRenderer body;

    public virtual void SetSortingLayerById(int id)
    {
        if (body.sortingLayerID == id) return;

        body.sortingLayerID = id;
    }

    public virtual void SetView()
    {
        // get random pack
        int index = Random.Range(0, packs.Length);
        packs[index].SetSprites(new SpriteRenderer[1] { body });
    }
}
