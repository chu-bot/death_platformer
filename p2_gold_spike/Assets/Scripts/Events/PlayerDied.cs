using UnityEngine;

public class PlayerDiedEvent
{
    public Vector2 deathPosition;

    public PlayerDiedEvent(Vector2 pos)
    {
        deathPosition = pos;
    }

    public override string ToString()
    {
        return $"Player died at {deathPosition}";
    }
}
