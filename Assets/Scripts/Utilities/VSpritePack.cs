using UnityEngine;


namespace Utilites.SpritePacks
{

    [CreateAssetMenu(fileName = "New VSpritePack", menuName = "Unit/SpritePack/VSpritePack")]
    [System.Serializable]
    public class VSpritePack : ScriptableObject
    {
        [SerializeField] private Sprite body;
        public virtual void SetSprites(SpriteRenderer[] renderers)
        {
            if (renderers.Length == 0) return;

            renderers[0].sprite = body;
        }
    }
}

//[CreateAssetMenu(fileName = "New VSpritePackWithLightCover", menuName = "Unit / VSpritePackWithLightCover")]
//[System.Serializable]
//public class VSpritePackWithLightCover : VSpritePack
//{
//    [SerializeField] protected private Sprite lightCover;

//    public override void SetSprites(SpriteRenderer[] renderers)
//    {
//        base.SetSprites(renderers);
//        SetCovers(renderers);
//    }
     
//    protected virtual void SetCovers(SpriteRenderer[] renderers)
//    {
//        if (renderers.Length < 3) return;

//        renderers[1].sprite = lightCover;
//        renderers[2].sprite = lightCover;
//    }
//}

//[CreateAssetMenu(fileName = "New VSpritePackWithTwoLightCovers", menuName = "Unit / VSpritePackWithTwoLightCovers")]
//[System.Serializable]
//public class VSpritePackWithTwoLightCovers : VSpritePackWithLightCover
//{
//    [SerializeField] private Sprite rightlightCover;

//    protected override void SetCovers(SpriteRenderer[] renderers)
//    {
//        if (renderers.Length < 3) return;

//        renderers[1].sprite = lightCover;
//        renderers[2].sprite = rightlightCover;
//    }
//}

