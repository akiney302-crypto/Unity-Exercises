using UnityEngine;

public class ItemSunRay : Item
{
    #region Constants
    const float SUNRAY_ENERGY = 10;
    #endregion

    #region Properties
    [SerializeField] private float _horizontalSpeed = 2f;
    [SerializeField] private float _maxFallSpeed = 2f;

    private Rigidbody2D rb;
    private float _horizontalDirection;
    #endregion


    #region Unity Callbacks
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Collider2D itemCollider = GetComponent<Collider2D>();
        if (itemCollider != null)
            itemCollider.sharedMaterial = null;

        _horizontalDirection = Random.value < 0.5f ? -1f : 1f;
        rb.linearVelocity = new Vector2(_horizontalDirection * _horizontalSpeed, rb.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        if (rb == null)
            return;

        Vector2 velocity = rb.linearVelocity;
        velocity.x = _horizontalDirection * _horizontalSpeed;
        if (velocity.y < -_maxFallSpeed)
            velocity.y = -_maxFallSpeed;
        
        rb.linearVelocity = velocity;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (rb != null)
            {
                _horizontalDirection = transform.position.x < collision.transform.position.x ? -1f : 1f;
                Vector2 velocity = rb.linearVelocity;
                velocity.x = _horizontalDirection * _horizontalSpeed;
                rb.linearVelocity = velocity;

                

            }
            return;
        }

        if (collision.gameObject.tag == "Ground")
            Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            Wings wings = collision.gameObject.GetComponent<Wings>();
            if (wings != null)
                wings.Regenerate(SUNRAY_ENERGY);
            wings.RemoveWet();
            
            Recolected();
        }
    }

    #endregion
}
