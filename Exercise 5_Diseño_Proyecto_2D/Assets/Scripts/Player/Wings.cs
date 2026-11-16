using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class Wings : MonoBehaviour
{
    #region Properties
    public float Energy { get; private set; }
    public bool IsFlying { get; private set; }
    #endregion

    #region Fields
    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _energyFlyingRate = 20f;
    [SerializeField] private float _energyRegenerationRate = 10f;
    [SerializeField] private float _flyForce = 12f;
    [SerializeField] private float _maxFlySpeed = 5f;

    private Rigidbody2D rb;
    private bool _isGrounded;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Energy = _maxEnergy;
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            StopFlying();
            Regenerate(_energyRegenerationRate * Time.fixedDeltaTime);
            return;
        }

        if (IsFlying)
            Fly();
        
    }
    #endregion

    #region Public Methods
    public void SetFlightRequested(bool requested, bool grounded)
    {
        _isGrounded = grounded;
        IsFlying = requested && !_isGrounded && Energy > 0f;
    }

    public void FlyUp()
    {
        IsFlying = true;

    }

    public void StopFlying()
    {
        IsFlying = false;
    }

    public void Regenerate(float energy)
    {
        Energy += energy;
        Energy = Mathf.Clamp(Energy, 0, _maxEnergy);
    }
    #endregion

    #region Private Methods
    private void Fly()
    {
        if (Energy <= 0f)
        {
            StopFlying();
            return;
        }

        rb.AddForce(Vector2.up * _flyForce * rb.mass, ForceMode2D.Force);

        if(rb.linearVelocity.y > _maxFlySpeed)        
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, _maxFlySpeed);

        Energy -= _energyFlyingRate * Time.fixedDeltaTime;
    }
    #endregion  
}
