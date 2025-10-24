using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerResizer : MonoBehaviour
{
    private BoxCollider2D bc;
    private Transform body; // direct child marked by BodyMarker

    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();

        foreach (Transform child in transform)
        {
            if (child.GetComponent<BodyMarker>())
            {
                body = child;
                break;
            }
        }

        if (!body) 
        {
            Debug.LogError("PlayerResizer: No direct child with BodyMarker found.", this);
        }
    }

    public void Resize(Vector2 newSizeWorld)
    {
        bc.size = newSizeWorld;
        bc.offset = Vector2.zero;

        body.localScale = new Vector3(newSizeWorld.x, newSizeWorld.y, 1f);

        var gd = GetComponent<GroundDetector>();
        if (gd) 
        {
            gd.Init(newSizeWorld);
        }
    }
}
