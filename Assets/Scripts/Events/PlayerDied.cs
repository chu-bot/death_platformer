using UnityEngine;

public class PlayerDiedEvent
{
    public Vector2 deathPosition;
    public Vector2 playerSize;

    public PlayerDiedEvent(Vector2 deathPosition, Vector2 playerSize)
    {
        this.deathPosition = deathPosition;
        this.playerSize = playerSize;
    }

    public override string ToString()
    {
        return $"Player died at {deathPosition}";
    }
}
