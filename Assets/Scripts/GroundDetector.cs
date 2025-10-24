using UnityEngine;

class GroundDetector : MonoBehaviour
{
    [HideInInspector] public bool grounded = false;

    private Vector2 centerOffset;
    private Vector2 boxSize;
    private float boxHeight = 0.1f;
    private Vector2 currentPos => (Vector2)transform.TransformPoint(centerOffset);
    
    private Vector2 leftWallOffset;
    private Vector2 rightWallOffset;
    private Vector2 wallCheckSize;
    private float wallCheckDistance = 0.05f;

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
        boxSize = new Vector2(playerSize.x * 0.95f, boxHeight);
        
        leftWallOffset = new Vector2(-(playerSize.x/2f + wallCheckDistance), 0);
        rightWallOffset = new Vector2(playerSize.x/2f + wallCheckDistance, 0);
        wallCheckSize = new Vector2(wallCheckDistance, playerSize.y * 0.8f);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapBox(currentPos, boxSize, 0, groundMask) != null;

        if (grounded) coyoteBufferTime = MAXCOYOTEBUFFER;
        else coyoteBufferTime -= Time.fixedDeltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);
        Gizmos.DrawCube(currentPos, boxSize);
        
        Gizmos.color = new Color(0.0f, 0.75f, 0.0f, 0.75f);
        Vector2 leftWallPos = (Vector2)transform.TransformPoint(leftWallOffset);
        Vector2 rightWallPos = (Vector2)transform.TransformPoint(rightWallOffset);
        Gizmos.DrawCube(leftWallPos, wallCheckSize);
        Gizmos.DrawCube(rightWallPos, wallCheckSize);
    }

    public bool CanJump() 
    {
        return coyoteBufferTime > 0f || grounded;
    }
    
    public bool IsTouchingLeftWall()
    {
        Vector2 leftWallPos = (Vector2)transform.TransformPoint(leftWallOffset);
        return Physics2D.OverlapBox(leftWallPos, wallCheckSize, 0, groundMask) != null;
    }
    
    public bool IsTouchingRightWall()
    {
        Vector2 rightWallPos = (Vector2)transform.TransformPoint(rightWallOffset);
        return Physics2D.OverlapBox(rightWallPos, wallCheckSize, 0, groundMask) != null;
    }
    
    public bool IsTouchingWall()
    {
        return IsTouchingLeftWall() || IsTouchingRightWall();
    }


}