using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class Wings : MonoBehaviour
{
    #region Properties
    public float Energy { get; set; }
    public bool IsFlying { get; set; }
    public bool IsStunned { get; set; }
    public bool IsWet { get; set; }

    public float MovRalent => IsWet ? 0.5f : 1f;
    #endregion

    #region Fields
    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _energyFlyingRate = 20f;
    [SerializeField] private float _energyRegenerationRate = 10f;
    [SerializeField] private float _flyForce = 12f;
    [SerializeField] private float _maxFlySpeed = 5f;
    [SerializeField] private float _wetMassMultiplier = 1.5f;

    private Rigidbody2D rb;
    private bool _isGrounded;
    private float _baseMass;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Energy = _maxEnergy;
        _baseMass = rb.mass;
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
        if (IsStunned)
        {
            StopFlying();
            return;
        }
        IsFlying = requested && !_isGrounded && Energy > 0f && !IsStunned;
    }

    public void FlyUp()
    {
        IsFlying = true;

    }

    public void StopFlying()
    {
        IsFlying = false;
    }

    public void Stun(float duration)
    {
        IsStunned = true;
        StopFlying();
        CancelInvoke(nameof(RecoverFromStun));
        Invoke(nameof(RecoverFromStun), duration);
    }

    public void Wet(float duration)
    {
        IsWet = true;
        rb.mass = _baseMass * _wetMassMultiplier;
        CancelInvoke(nameof(RemoveWet));
        Invoke(nameof(RemoveWet), duration);
    }

    public void RemoveWet()
    {
        CancelInvoke(nameof(RemoveWet));
        IsWet = false;
        rb.mass = _baseMass;
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

    private void RecoverFromStun()
    {
        IsStunned = false;
    }
    #endregion  
}
