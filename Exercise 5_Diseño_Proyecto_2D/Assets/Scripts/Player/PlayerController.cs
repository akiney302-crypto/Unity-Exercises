using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 9.5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckWidth = 0.8f;
    [SerializeField] private float groundCheckHeight = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Wings wings;

    #endregion



    #region Private variables
    private Rigidbody2D rb;

    private float horizontalInput;
    private bool isGrounded;
    private bool jumpRequested;
    private bool jumpHeld;

    #endregion


    #region Unity Callbacks
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ReadInput();
        SpriteFlip();
        CheckGround();

        wings.SetFlightRequested(jumpHeld, isGrounded);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void ReadInput()
    {
        Keyboard keyboard = Keyboard.current;

        if(keyboard == null)
        { 
            horizontalInput = 0f;
            jumpHeld = false;
            return; 
        }

        horizontalInput = 0f;


        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            horizontalInput = -1f;
        }
        else if(keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            horizontalInput = 1f;
        }

        jumpHeld = keyboard.spaceKey.isPressed;

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0f, groundLayer) != null;
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void SpriteFlip()
    {
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    private void Jump()
    {
        if(jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        jumpRequested = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(groundCheck.position, new Vector3(groundCheckWidth, groundCheckHeight, 0f));
    }
    #endregion
}