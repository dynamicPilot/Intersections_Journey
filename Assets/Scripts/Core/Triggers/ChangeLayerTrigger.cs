using UnityEngine;

public class ChangeLayerTrigger : MonoBehaviour
{
    [SerializeField] private string newLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D)
        {
            collision.gameObject.GetComponent<VEffects>().SetSortingLayerById(SortingLayer.NameToID(newLayer));
        }
    }
}
