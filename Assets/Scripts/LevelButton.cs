using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [Header("Level Settings")]
    public int sceneNumber = 1; // Public input for scene number

    public void LoadLevel()
    {
        Debug.Log($"Loading Scene {sceneNumber}");
        SceneManager.LoadScene(sceneNumber);
    }
}
