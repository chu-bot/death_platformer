using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerFactory))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Defaults")]
    [SerializeField] private Vector2 defaultPlayerSize = new Vector2(1f, 1f);
    
    [Header("Respawn Settings")]
    [SerializeField] private int maxRespawns = 5;
    [SerializeField] private bool gameOver = false;
    
    [Header("Game Over Scene")]
    [SerializeField] private int gameOverSceneIndex = -1;
    
    [Header("Win Scene")]
    [SerializeField] private int winSceneIndex = -1;
    [SerializeField] private int maxLevelIndex = -1;

    private PlayerFactory factory;
    private Player currentPlayer;
    private int currentRespawns = 0;
    private int totalDeaths = 0;
    private Subscription<PlayerDiedEvent> playerDiedSubscription;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            factory = GetComponent<PlayerFactory>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            playerDiedSubscription = EventBus.Subscribe<PlayerDiedEvent>(OnPlayerDied);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            if (playerDiedSubscription != null)
            {
                EventBus.Unsubscribe<PlayerDiedEvent>(playerDiedSubscription);
            }
            Instance = null;
        }
    }

    void Start()
    {
        SpawnForCurrentScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetLivesForNewScene();
        SpawnForCurrentScene();
    }

    private void ResetLivesForNewScene()
    {
        currentRespawns = 0;
        gameOver = false;
        Debug.Log("Lives reset for new scene - starting with full lives");
    }

    private void SpawnForCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (currentSceneIndex > maxLevelIndex || currentSceneIndex == winSceneIndex || currentSceneIndex == 0)
        {
            Debug.Log($"Not spawning player in scene {currentSceneIndex} (max level: {maxLevelIndex}, win scene: {winSceneIndex})");
            return;
        }

        if (currentPlayer)
        {
            Destroy(currentPlayer.gameObject);
            currentPlayer = null;
        }

        var position = GameObject.FindGameObjectWithTag("SpawnPoint");

        Vector2 pos = position.transform.position;
        currentPlayer = factory.SpawnAt(pos, defaultPlayerSize);
    }

    private void OnPlayerDied(PlayerDiedEvent playerDiedEvent)
    {
        if (gameOver) 
        {
            return;
        }

        currentRespawns++;
        totalDeaths++;
        
        if (currentRespawns > maxRespawns)
        {
            gameOver = true;
            EventBus.Publish(new GameOverEvent());
            LoadGameOverScene();
            Debug.Log($"Game Over! Player died {currentRespawns} times (max allowed: {maxRespawns}). Total deaths: {totalDeaths}");
        }
        else
        {
            Debug.Log($"Player died. Respawn {currentRespawns}/{maxRespawns}. Total deaths: {totalDeaths}");
            RespawnPlayer(playerDiedEvent.deathPosition, playerDiedEvent.playerSize);
        }
    }

    private void RespawnPlayer(Vector2 deathPosition, Vector2 playerSize)
    {
        var spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        Vector2 spawnPos = spawnPoint ? spawnPoint.transform.position : deathPosition;

        if (currentPlayer)
        {
            Destroy(currentPlayer.gameObject);
            currentPlayer = null;
        }

        currentPlayer = factory.SpawnAt(spawnPos, playerSize);
    }

    private void LoadGameOverScene()
    {
        if (gameOverSceneIndex >= 0)
        {
            SceneManager.LoadScene(gameOverSceneIndex);
        }
        else
        {
            Debug.LogWarning("Game Over Scene Index not set! Please set the gameOverSceneIndex in GameManager.");
        }
    }

    public void LoadWinScene()
    {
        if (winSceneIndex >= 0)
        {
            SceneManager.LoadScene(winSceneIndex);
        }
        else
        {
            Debug.LogWarning("Win Scene Index not set! Please set the winSceneIndex in GameManager.");
        }
    }

    public void ResetGame()
    {
        gameOver = false;
        currentRespawns = 0;
        
        Time.timeScale = 1f;
        
        SpawnForCurrentScene();
    }

    public int GetCurrentRespawns() 
    {
        return currentRespawns;
    }
    
    public int GetMaxRespawns() 
    {
        return maxRespawns;
    }
    
    public int GetTotalDeaths() 
    {
        return totalDeaths;
    }
    
    public bool IsGameOver() 
    {
        return gameOver;
    }

    public void ChangeMaxLives(int amount)
    {
        maxRespawns += amount;
        EventBus.Publish(new MaxLivesChangedEvent(maxRespawns));
    }

    public string GetMaxLivesString()
    {
        return $"Max lives: {maxRespawns}";
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(0);
    }

    public void StartFromBeginning()
    {
        gameOver = false;
        currentRespawns = 0;
        totalDeaths = 0;
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(1);
    }
}
