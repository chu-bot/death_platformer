using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickRestart : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu!");
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(0);
    }
}
