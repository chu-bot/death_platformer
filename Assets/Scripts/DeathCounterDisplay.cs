using UnityEngine;
using TMPro;

public class DeathCounterDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathText;
    
    void Start()
    {
        if (deathText == null)
        {
            deathText = GetComponent<TextMeshProUGUI>();
        }
        
        UpdateDeathDisplay();
    }
    
    void Update()
    {
        UpdateDeathDisplay();
    }
    
    private void UpdateDeathDisplay()
    {
        if (deathText != null && GameManager.Instance != null)
        {
            int totalDeaths = GameManager.Instance.GetTotalDeaths();

            if (totalDeaths == 0) {
                deathText.text = $"You killed {totalDeaths} Blockians :)))";
            }
            else if (totalDeaths == 1) {
                deathText.text = $"You killed {totalDeaths} Blockian :(";
            }
            else if (totalDeaths <= 5) {
                deathText.text = $"You killed {totalDeaths} Blockians. They had tiny hopes :(";
            }
            else if (totalDeaths <= 15) {
                deathText.text = $"You killed {totalDeaths} Blockians. That’s a small neighborhood :(";
            }
            else if (totalDeaths <= 30) {
                deathText.text = $"You killed {totalDeaths} Blockians. The memorial needs extra pages :(";
            }
            else if (totalDeaths <= 60) {
                deathText.text = $"You killed {totalDeaths} Blockians. This is officially a tragedy :(((";
            }
            else {
                deathText.text = $"You killed {totalDeaths} Blockians. Monster status unlocked :(((((";
            }

        }
    }
}
