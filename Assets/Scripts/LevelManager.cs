using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool loopAtEnd = false;

    private Subscription<ReachGoalEvent> goalSub;

    void Awake()
    {
        goalSub = EventBus.Subscribe<ReachGoalEvent>(OnGoalReached);
    }

    void OnDestroy()
    {
        if (goalSub != null)
        {
            EventBus.Unsubscribe(goalSub);
        }
    }

    private void OnGoalReached(ReachGoalEvent goalEvent)
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        int curr = SceneManager.GetActiveScene().buildIndex;
        int next = curr + 1;
        if (next >= SceneManager.sceneCountInBuildSettings)
        {
            if (!loopAtEnd) 
            { 
                Debug.Log("LevelManager: reached final level. Loading win scene.");
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.LoadWinScene();
                }
                else
                {
                    Debug.LogWarning("No GameManager found to load win scene!");
                }
                return; 
            }
            next = 0;
        }
        SceneManager.LoadScene(next);
    }

    public void ReloadLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(int buildIndex)
    {
        if (buildIndex < 0 || buildIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError($"LevelManager: invalid build index {buildIndex}");
            return;
        }
        SceneManager.LoadScene(buildIndex);
    }
}
