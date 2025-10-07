using UnityEngine;

public class SawTrap : Trap
{
    [Header("Saw Settings")]
    [SerializeField] private float rotationSpeed = 180f;

    void FixedUpdate()
    {
        // Rotate continuously around the Z-axis
        transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.fixedDeltaTime);
    }
}