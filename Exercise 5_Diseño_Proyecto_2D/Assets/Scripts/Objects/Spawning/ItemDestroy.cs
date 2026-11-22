using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f;

    private void Update()
    {
        CheckDirection(Vector2.left);
        CheckDirection(Vector2.right);    
    }

    private void CheckDirection(Vector2 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, rayDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;

            Item item = hit.collider.GetComponentInParent<Item>();
            if (item != null)
                Destroy(item.gameObject);
        }
    }
    
}
