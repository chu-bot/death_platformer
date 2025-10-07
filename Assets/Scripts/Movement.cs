using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;
    private Vector2 moveInput;
    private bool grounded;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [SerializeField] Canvas gameWon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnRespawn(InputValue value)
    {
        if (value.isPressed)
        {
            EventBus.Publish(new PlayerDiedEvent(transform.position));
        }
    }

    public void OnRestart(InputValue value)
    {
        RestartLevel.Restart();
    }

    public void OnJump()
    {
        if (grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        if (!gameWon.enabled)
        {   
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
            player.MoveEyes(new Vector2(moveInput.x, 0));
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("PlayerPlatform"))
            grounded = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            EventBus.Publish(new ReachGoalEvent());
            // player.currentLevel += 1;
            gameWon.enabled = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Platform")  || col.gameObject.CompareTag("PlayerPlatform"))
            grounded = false;
    }
}
