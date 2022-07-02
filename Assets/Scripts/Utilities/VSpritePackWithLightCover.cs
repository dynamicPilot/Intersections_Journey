using UnityEngine;


namespace Utilites.SpritePacks
{
    [CreateAssetMenu(fileName = "New VSpritePackWithLightCover", menuName = "Unit/SpritePack/VSpritePackWithLightCover")]
    [System.Serializable]
    public class VSpritePackWithLightCover : VSpritePack
    {
        [SerializeField] protected private Sprite lightCover;

        public override void SetSprites(SpriteRenderer[] renderers)
        {
            base.SetSprites(renderers);
            SetCovers(renderers);
        }

        protected virtual void SetCovers(SpriteRenderer[] renderers)
        {
            if (renderers.Length < 3) return;

            renderers[1].sprite = lightCover;
            renderers[2].sprite = lightCover;
        }
    }
}