using UnityEngine;
using TMPro;

public class MaxLivesDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text maxLivesText;
    private Subscription<MaxLivesChangedEvent> maxLivesSubscription;

    void Start()
    {
        if (maxLivesText == null)
        {
            maxLivesText = GetComponent<TMP_Text>();
        }
        
        maxLivesSubscription = EventBus.Subscribe<MaxLivesChangedEvent>(OnMaxLivesChanged);
        UpdateMaxLivesDisplay();
    }

    void OnDestroy()
    {
        if (maxLivesSubscription != null)
        {
            EventBus.Unsubscribe(maxLivesSubscription);
        }
    }

    private void OnMaxLivesChanged(MaxLivesChangedEvent maxLivesEvent)
    {
        UpdateMaxLivesDisplay();
    }

    private void UpdateMaxLivesDisplay()
    {
        if (maxLivesText != null && GameManager.Instance != null)
        {
            maxLivesText.text = GameManager.Instance.GetMaxLivesString();
        }
    }
}
