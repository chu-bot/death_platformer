using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectPanel;
    
    [Header("Main Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button levelSelectButton;
    
    [Header("Level Select Buttons")]
    [SerializeField] private Button backButton;

    void Start()
    {
        SetupButtons();
        ShowMainMenu();
    }

    private void SetupButtons()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayClicked);
        }
        
        if (levelSelectButton != null)
        {
            levelSelectButton.onClick.AddListener(OnLevelSelectClicked);
        }
        
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackClicked);
        }
    }

    public void OnPlayClicked()
    {
        Debug.Log("Starting from Level 1");
        SceneManager.LoadScene(1);
    }

    public void OnLevelSelectClicked()
    {
        ShowLevelSelect();
    }

    public void OnBackClicked()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
        
        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(false);
        }
    }

    private void ShowLevelSelect()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }
        
        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(true);
        }
    }
}
