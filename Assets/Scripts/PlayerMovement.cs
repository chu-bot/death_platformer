using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GroundDetector gd;
    private Player player;
    private Vector2 moveInput;

    [Header("Movement Settings")]
    [SerializeField] float groundMoveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;

    [Tooltip("Scalar applied to groundMoveSpeed to limit Air Movement")]
    [SerializeField] float horizontalAirResistance = 0.8f;

    private float HorizontalAirSpeed => groundMoveSpeed * horizontalAirResistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gd = GetComponent<GroundDetector>();
        player = GetComponent<Player>();
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    public void OnRespawn(InputValue value)
    {
        if (value.isPressed)
        {
            EventBus.Publish(new PlayerDiedEvent(transform.position, player.playerSize));
        }
    }

    public void OnRestart(InputValue value)
    {
        if (value.isPressed)
        {
            RestartLevel.Restart();
        }
    }

    public void OnJump()
    {
        if (gd.CanJump()) Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Jump() => rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * HorizontalAirSpeed, rb.linearVelocity.y);
        player.MoveEyes(new Vector2(moveInput.x, 0));
    }
}
