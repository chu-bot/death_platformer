using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject deathPrefab;

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
        Vector2 playerSize = e.playerSize;

        GameObject deathObject = Instantiate(deathPrefab, deathPos, Quaternion.identity);
        deathObject.GetComponent<PlayerResizer>().Resize(playerSize);
    }
}
