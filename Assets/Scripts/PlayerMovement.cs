using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    GroundDetector gd;
    Player player;

    Vector2 moveInput;

    [Header("Movement")]
    [SerializeField] float groundMoveSpeed = 8f;
    [SerializeField] float airMoveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] float jumpVelocity = 12f;
    
    [Header("Gravity")]
    [SerializeField] float baseGravity = 1f;
    [SerializeField] float fallGravity = 2.5f;
    [SerializeField] float jumpCutGravity = 3f;
    [SerializeField] float maxFallSpeed = 20f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gd = GetComponent<GroundDetector>();
        player = GetComponent<Player>();

        rb.gravityScale = baseGravity;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
    }

    public void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && gd.CanJump())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        }
    }


    public void OnRespawn(InputValue value)
    {
        if (value.isPressed)
        {
            RestartLevel.Restart();
        }
    }

    public void OnRestart(InputValue value)
    {
        if (value.isPressed) 
        {
            RestartLevel.Restart();
        }
    }

    void FixedUpdate()
    {
        HandleHorizontalMovement();
        HandleGravity();
        HandleWallSticking();
        
        player.MoveEyes(new Vector2(moveInput.x, 0));
    }

    private void HandleHorizontalMovement()
    {
        float moveSpeed = gd.grounded ? groundMoveSpeed : airMoveSpeed;
        float targetVelocity = moveInput.x * moveSpeed;
        
        rb.linearVelocity = new Vector2(targetVelocity, rb.linearVelocity.y);
    }

    private void HandleGravity()
    {
        if (rb.linearVelocity.y < -0.1f)
        {
            rb.gravityScale = fallGravity;
        }
        else if (rb.linearVelocity.y > 0.1f)
        {
            rb.gravityScale = baseGravity;
        }
        else
        {
            rb.gravityScale = baseGravity;
        }

        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
    }

    private void HandleWallSticking()
    {
        if (gd.IsTouchingWall())
        {
            if ((gd.IsTouchingLeftWall() && rb.linearVelocity.x < 0) || 
                (gd.IsTouchingRightWall() && rb.linearVelocity.x > 0))
            {
                float counterForce = 0.1f;
                Vector2 counterVelocity = new Vector2(-rb.linearVelocity.x * counterForce, 0);
                rb.linearVelocity += counterVelocity;
            }
        }
    }

}
