using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformSpawner))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(PlayerResizer))]
public class Player : MonoBehaviour
{
    public Vector2 playerSize;


    public int currentLevel = 1;

    [SerializeField] GameObject eyesObject;

    void Start()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    public void Init(Vector2 playerSize)
    {
        this.playerSize = playerSize;
    }

    public void MoveEyes(Vector2 xDir)
    {
        if (eyesObject == null) 
        {
            return;
        }

        float offsetAmount = 0.1f;
        Vector2 targetLocalPos = new Vector2(xDir.x * offsetAmount, 0f);
        eyesObject.transform.localPosition = targetLocalPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            EventBus.Publish(new ReachGoalEvent());
        }
        else if (collision.CompareTag("Trap") || collision.CompareTag("Projectile"))
        {
            EventBus.Publish(new PlayerDiedEvent(transform.position, playerSize));
        }
    }
}
