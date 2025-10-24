using UnityEngine;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Collider2D colliderToDisable;
    [SerializeField] private float flashDuration = 1f;
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        
        FlashWithCollider(colliderToDisable, flashDuration);
    }
    
    public void FlashWithCollider(Collider2D colliderToDisable, float duration = 1f)
    {
        StartCoroutine(FlashCoroutine(colliderToDisable, duration));
    }
    
    private IEnumerator FlashCoroutine(Collider2D colliderToDisable, float duration)
    {
        if (spriteRenderer == null) 
        {
            yield break;
        }
        
        while (true)
        {
            bool wasColliderEnabled = false;
            if (colliderToDisable != null)
            {
                wasColliderEnabled = colliderToDisable.enabled;
                colliderToDisable.enabled = false;
            }
            
            Color flashColor = originalColor;
            flashColor.a = 0.5f;
            spriteRenderer.color = flashColor;
            
            yield return new WaitForSeconds(duration);
            
            spriteRenderer.color = originalColor;
            
            if (colliderToDisable != null && wasColliderEnabled)
            {
                colliderToDisable.enabled = true;
            }
            
            yield return new WaitForSeconds(duration);
        }
    }
}
