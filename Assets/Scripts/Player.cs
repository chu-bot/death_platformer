using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 spawnPoint;

    public int currentLevel = 1;

    [SerializeField] GameObject eyesObject;

    void Start()
    {
        Screen.SetResolution(1920, 1080, false);
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
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

    public void SetSpawnPoint(Vector3 newPoint)
    {
        spawnPoint = newPoint;
    }
}
