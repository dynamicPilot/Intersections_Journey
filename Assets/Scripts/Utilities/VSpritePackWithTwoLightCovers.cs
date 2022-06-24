using UnityEngine;

namespace Utilites.SpritePacks
{
    [CreateAssetMenu(fileName = "New VSpritePackWithTwoLightCovers", menuName = "Unit / VSpritePackWithTwoLightCovers")]
    [System.Serializable]
    public class VSpritePackWithTwoLightCovers : VSpritePackWithLightCover
    {
        [SerializeField] private Sprite rightlightCover;

        protected override void SetCovers(SpriteRenderer[] renderers)
        {
            if (renderers.Length < 3) return;

            renderers[1].sprite = lightCover;
            renderers[2].sprite = rightlightCover;
        }
    }
}


