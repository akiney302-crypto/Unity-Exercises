using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStatus))]

public class Wings : MonoBehaviour
{
    #region Properties
    public float Energy { get; set; }
    public bool IsFlying { get; set; }
    public bool IsStunned => _status != null && _status.IsStunned;
    public bool IsWet => _status != null && _status.IsWet;

    public float MovRalent => _status != null ? _status.MovRalent : 1f;
    #endregion

    #region Fields
    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _energyFlyingRate = 20f;
    [SerializeField] private float _energyRegenerationRate = 10f;
    [SerializeField] private float _flyForce = 12f;
    [SerializeField] private float _maxFlySpeed = 5f;
    [SerializeField] private float _wetMassMultiplier = 1.5f;

    private Rigidbody2D rb;
    private PlayerStatus _status;
    private bool _isGrounded;
    private float _baseMass;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _status = GetComponent<PlayerStatus>();

        Energy = _maxEnergy;
        _baseMass = rb.mass;
    }

    private void OnEnable()
    {
        _status = GetComponent<PlayerStatus>();

        if (_status != null)
        {
            _status.StunChanged += HandleStunChanged;
            _status.WetChanged += HandleWetChanged;
        }
    }

    private void OnDisable()
    {
        if (_status != null)
        {
            _status.StunChanged -= HandleStunChanged;
            _status.WetChanged -= HandleWetChanged;
        }
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
        _status?.Stun(duration);
    }

    public void Wet(float duration)
    {
        _status?.Wet(duration);
    }

    public void RemoveWet()
    {
        _status?.RemoveWet();
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

    private void HandleStunChanged(bool stunned)
    {
        if (stunned)
            StopFlying();
    }

    private void HandleWetChanged(bool wet)
    {
        rb.mass = wet ? _baseMass * _wetMassMultiplier : _baseMass;
    }
    #endregion  
}
