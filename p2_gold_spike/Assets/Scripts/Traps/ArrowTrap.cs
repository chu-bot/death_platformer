using Unity.Mathematics;
using UnityEngine;

public class ArrowTrap : Trap
{
    [Header("Arrow Settings")]
    [SerializeField] private GameObject projectilePrefab;

    [Tooltip("Arrow Direction")]
    [SerializeField] private Vector2 shootDirection = Vector2.right;

    [Tooltip("Arrow Velocity")]
    [SerializeField] private float arrowVelocity = 2f;

    [Tooltip("Time interval between shots (seconds)")]
    [SerializeField] private float fireInterval = 2f;

    private float timeSinceLastShot;

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= fireInterval)
        {
            FireArrow();
            timeSinceLastShot = 0f;
        }
    }

    private void FireArrow()
    {
        if (projectilePrefab == null) return;

        // Spawn arrow
        Vector3 spawnPos = transform.position;
        GameObject arrow = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        arrow.GetComponent<Projectile>().Init(shootDirection, arrowVelocity);
        
    }
}
