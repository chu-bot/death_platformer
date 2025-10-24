using UnityEngine;
using TMPro;

public class LivesDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text livesText;
    
    [Header("Display Settings")]
    [SerializeField] private string livesFormat = "Lives Remaining: {0}";

    void Start()
    {
        if (livesText == null)
        {
            livesText = GetComponent<TMP_Text>();
        }
        
        UpdateLivesDisplay();
    }

    void Update()
    {
        UpdateLivesDisplay();
    }

    private void UpdateLivesDisplay()
    {
        if (livesText == null || GameManager.Instance == null) 
        {
            return;
        }

        int currentRespawns = GameManager.Instance.GetCurrentRespawns();
        int maxRespawns = GameManager.Instance.GetMaxRespawns();
        int livesRemaining = maxRespawns - currentRespawns;
        
        livesRemaining = Mathf.Max(0, livesRemaining);
        
        livesText.text = string.Format(livesFormat, livesRemaining);
    }
}
