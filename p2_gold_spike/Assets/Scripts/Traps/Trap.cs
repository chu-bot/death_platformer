using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";

    // Called when another collider enters this trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            EventBus.Publish(new PlayerDiedEvent(collision.transform.position));
            Debug.Log($"[Trap] {name} triggered by {collision.name}");
        }
    }
}
