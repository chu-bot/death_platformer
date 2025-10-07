using UnityEngine;

public class TemporaryPlatform : MonoBehaviour
{
    private float lifetime;
    private float flashStartTime;
    private float flashSpeed;
    private SpriteRenderer sr;

    private bool isFlashing;

    public void Init(float lifeTime, float flashStart, float flashSpeed)
    {
        this.lifetime = lifeTime;
        this.flashStartTime = flashStart;
        this.flashSpeed = flashSpeed;

        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(Lifecycle());
    }

    private System.Collections.IEnumerator Lifecycle()
    {
        // Wait until flashing time
        yield return new WaitForSeconds(flashStartTime);

        // Start flashing effect
        isFlashing = true;
        float elapsed = 0f;

        while (elapsed < lifetime - flashStartTime)
        {
            elapsed += Time.deltaTime;

            if (sr != null)
            {
                float alpha = Mathf.Abs(Mathf.Sin(Time.time * flashSpeed)); // thanks for the suggestion GPT
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
