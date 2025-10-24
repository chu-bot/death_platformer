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

    public void Init(Vector2 dir, float velocity)
    {
        direction = dir.normalized;
        speed = velocity;

        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
