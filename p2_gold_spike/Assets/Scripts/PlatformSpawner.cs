using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject platformPrefab;
    [Tooltip("Platform Lifetime")]
    [SerializeField] private float platformLifetime = 12f;
    [Tooltip("When to flash")]
    [SerializeField] private float flashStartTime = 9f;
    [Tooltip("Flashing speed (times per second).")]
    [SerializeField] private float flashSpeed = 5f;

    private Subscription<PlayerDiedEvent> deathEvent;

    void OnEnable()
    {
        deathEvent = EventBus.Subscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe(deathEvent);
    }

    void OnPlayerDied(PlayerDiedEvent e)
    {
        Vector3 deathPos = e.deathPosition;
        deathPos.y += 0.2f; // slight height adjustment

        GameObject platform = Instantiate(platformPrefab, deathPos, Quaternion.identity);
        TemporaryPlatform temp = platform.AddComponent<TemporaryPlatform>();

        temp.Init(platformLifetime, flashStartTime, flashSpeed);
    }
}
