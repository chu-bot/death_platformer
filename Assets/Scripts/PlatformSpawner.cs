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
        Vector2 deathPos = e.deathPosition;
        Vector2 playerSize = e.deathPosition;

        Instantiate(platformPrefab, deathPos, Quaternion.identity);

        // temp.Init(platformLifetime, flashStartTime, flashSpeed);
    }
}
