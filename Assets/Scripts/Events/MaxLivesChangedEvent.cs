using UnityEngine;

public class MaxLivesChangedEvent
{
    public int newMaxLives;

    public MaxLivesChangedEvent(int newMaxLives)
    {
        this.newMaxLives = newMaxLives;
    }

    public override string ToString()
    {
        return $"Max lives changed to {newMaxLives}";
    }
}



