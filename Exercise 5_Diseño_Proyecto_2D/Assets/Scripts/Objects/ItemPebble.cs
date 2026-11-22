using UnityEngine;

public class ItemPebble : Item
{
    #region Constants
    const float PEBBLE_FORCE = 10;
    const float PEBBLE_STUN_TIME = 1;
    #endregion

    #region Properties
    [SerializeField] private float _minPebbleSize = 0.8f;
    [SerializeField] private float _maxPebbleSize = 1.2f;
    [SerializeField] private float _horizontalSpeed = 2f;

    private float _sizeMultiplier = 1f;
    private Vector3 _baseScale;
    private Rigidbody2D rb;
    private bool _hasLanded;
    private float _horizontalDirection;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _baseScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Recolected();
        if (collision.gameObject.tag == "Platform")
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.GetContact(i).normal.y > 0.5f && !_hasLanded)
                {
                    _hasLanded = true;
                    _horizontalDirection = Random.value < 0.5f ? -1f : 1f;
                    rb.linearVelocity = new Vector2(_horizontalDirection * _horizontalSpeed, rb.linearVelocity.y);
                    break;
                }
            }
        }
        else if(collision.gameObject.tag =="Wall" && _hasLanded)
        {
            _horizontalDirection *= -1f;
            rb.linearVelocity = new Vector2(_horizontalDirection * _horizontalSpeed, rb.linearVelocity.y);
        }

        if (collision.gameObject.tag == "Player")
        {
            Transform pebblePos = GetComponent<Transform>();
            Transform playerPos = collision.gameObject.GetComponent<Transform>();

            if (playerPos.position.y > pebblePos.position.y)
            {
                Recolected();
                return;
            }

            Wings wings = collision.gameObject.GetComponent<Wings>();

            if (wings != null && wings.IsFlying)
            {
                wings.GetComponent<Rigidbody2D>().AddForce(Vector2.down * PEBBLE_FORCE * _sizeMultiplier);
            }
            PlayerStatus stunStatus = collision.gameObject.GetComponent<PlayerStatus>();
            if (stunStatus != null)
            {
                stunStatus.Stun(PEBBLE_STUN_TIME * _sizeMultiplier);
            }
            else if (wings != null)
            {
                wings.Stun(PEBBLE_STUN_TIME * _sizeMultiplier);
            }
            Recolected();

        }


    }
    #endregion

    #region Public Methods
    public void SetSize()
    {
        _sizeMultiplier = Random.Range(_minPebbleSize, _maxPebbleSize);
        transform.localScale = _baseScale * _sizeMultiplier;
    }

    #endregion


}



