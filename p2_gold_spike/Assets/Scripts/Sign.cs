using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sign : MonoBehaviour
{
    [Header("Sign Settings")]
    [TextArea] public string message = "This is a sign!";
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Canvas signCanvas;

    void Start()
    {
        if (signCanvas != null)
            signCanvas.enabled = false; // start hidden
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowMessage();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideMessage();
        }
    }

    private void ShowMessage()
    {
        if (signCanvas != null)
        {
            signCanvas.enabled = true;
            if (messageText != null)
                messageText.text = message;
        }
    }

    private void HideMessage()
    {
        if (signCanvas != null)
            signCanvas.enabled = false;
    }
}
