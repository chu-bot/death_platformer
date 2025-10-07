using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called by whoever spawns this projectile
    public void Init(Vector2 dir, float velocity)
    {
        direction = dir.normalized;
        speed = velocity;

        // Apply velocity immediately
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Example: kill player if hit
        if (other.CompareTag("Player"))
        {
            EventBus.Publish(new PlayerDiedEvent(other.transform.position));
        }

        else if (other.CompareTag("PlayerPlatform"))
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
