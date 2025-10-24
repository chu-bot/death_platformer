using UnityEngine;

public class ChangeMaxLives : MonoBehaviour
{
    public void ChangeLives(int amount)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeMaxLives(amount);
        }
    }
}



