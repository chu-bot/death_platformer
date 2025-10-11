using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlatformSpawner))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundDetector))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private GroundDetector groundDetector;
    private Vector2 spawnPoint;

    public Vector2 playerSize;


    public int currentLevel = 1;

    [SerializeField] GameObject eyesObject;

    void Start()
    {
        Screen.SetResolution(1920, 1080, false); // figure out a better place to put this
        rb = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<GroundDetector>();
        groundDetector.Init(playerSize);
    }

    void Init(Vector2 spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    void OnEnable()
    {
        // Subscribe to the death event so we can respawn
        EventBus.Subscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    public void MoveEyes(Vector2 xDir)
    {
        if (eyesObject == null) return;

        float offsetAmount = 0.05f;

        Vector2 targetLocalPos = new Vector2(xDir.x * offsetAmount, 0f);

        eyesObject.transform.localPosition = targetLocalPos;
    }


    private void OnPlayerDied(PlayerDiedEvent e)
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = spawnPoint;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            EventBus.Publish(new ReachGoalEvent());
            // player.currentLevel += 1;
            // gameWon.enabled = true;
        }
        else if (collision.CompareTag("Trap") || collision.CompareTag("Projectile"))
        {
            EventBus.Publish(new PlayerDiedEvent(transform.position, playerSize));
        }
    }
}
