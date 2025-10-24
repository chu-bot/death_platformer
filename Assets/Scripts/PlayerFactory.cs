using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [Header("Prefab & Defaults")]
    [SerializeField] private Player playerPrefab;
    private Vector2 defaultPlayerSize = new Vector2(1f, 1f);

    public readonly List<Player> SpawnedPlayers = new List<Player>();

    public Player SpawnAt(Vector2 spawnPos, Vector2? sizeOverride = null)
    {
        var size = sizeOverride ?? defaultPlayerSize;

        GameObject playerObj = Instantiate(playerPrefab.gameObject);
        var playerScript = playerObj.GetComponent<Player>();
        var resizeScript = playerObj.GetComponent<PlayerResizer>();
        playerObj.SetActive(false);

        playerObj.transform.position = spawnPos;
        playerScript.Init(size);
        resizeScript.Resize(size);

        playerObj.SetActive(true);
        SpawnedPlayers.Add(playerScript);
        return playerScript;
    }
}
