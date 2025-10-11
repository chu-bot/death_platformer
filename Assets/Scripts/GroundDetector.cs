using UnityEngine;

class GroundDetector : MonoBehaviour
{
    // exposed boolean to check for grounded
    [HideInInspector] public bool grounded = false;


    // Box-Offset 
    private Vector2 centerOffset;
    private Vector2 boxSize;
    // constant, represents height of box for overlap
    private float boxHeight = 0.1f;
    private Vector2 currentPos => (Vector2)transform.TransformPoint(centerOffset);

    // coyote jump logic
    private float coyoteBufferTime = 0.0f;
    private float MAXCOYOTEBUFFER = 0.14f;

    private LayerMask groundMask;

    private void Awake()
    {
        groundMask = LayerMask.GetMask("Terrain");
    }

    public void Init(Vector2 playerSize)
    {
        centerOffset = new Vector2(0, -(playerSize.y/2f) - boxHeight/2f);
        boxSize = new Vector2(playerSize.x * 0.8f, boxHeight);
    }

    void FixedUpdate()
    {
        var prevState = grounded;
        grounded = Physics2D.OverlapBox(currentPos, boxSize, 0, groundMask) != null;

        // coyote jump logic
        if (prevState == true && grounded == false)
        {
            coyoteBufferTime = MAXCOYOTEBUFFER;
        }

        if (coyoteBufferTime > 0f)
        {
            coyoteBufferTime -= Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);
        Gizmos.DrawCube(currentPos, boxSize);
    }

    public bool CanJump() => coyoteBufferTime > 0f || grounded;


}