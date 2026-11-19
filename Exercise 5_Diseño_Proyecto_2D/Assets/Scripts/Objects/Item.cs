using UnityEngine;

public class Item : MonoBehaviour
{
    #region Constants
    const float PEBBLE_FORCE = 10;
    const float PEBBLE_STUN_TIME = 1;
    const float WATER_WET = -10;
    const float WATER_WET_DURATION = 4;
    const float SUNRAY_ENERGY = 10;
    #endregion

    #region Enums
    public enum ItemType
    {
        None,
        Pebble,
        Sunray,
        Water
    }
    #endregion


    #region Properties
    [field: SerializeField] public ItemType Type { get; private set; }

    [SerializeField] private float _minPebbleSize = 0.8f;
    [SerializeField] private float _maxPebbleSize = 1.2f;

    private float _sizeMultiplier = 1f;
    private Vector3 _baseScale;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        _baseScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Wings wings = collision.gameObject.GetComponent<Wings>();
            switch (Type)
            {
                case ItemType.Pebble:
                    if (wings.IsFlying)
                        wings.GetComponent<Rigidbody2D>().AddForce(Vector2.down * PEBBLE_FORCE * _sizeMultiplier);
                    wings.Stun(PEBBLE_STUN_TIME * _sizeMultiplier);
                    break;

                case ItemType.Sunray:
                    wings.Regenerate(SUNRAY_ENERGY);
                    break;

                case ItemType.Water:
                    wings.Regenerate(WATER_WET);
                    break;

            }

            Destroy(gameObject);

            if (collision.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
            }
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