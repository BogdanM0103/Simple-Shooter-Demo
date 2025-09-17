using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    // Prefab of the robot enemy to spawn
    [SerializeField] GameObject robotPrefab;

    // Time (in seconds) between spawns
    [SerializeField] float spawnTime = 5f;

    // Location where the robot will spawn
    [SerializeField] Transform spawnPoint;

    // Reference to the player's health (used to check if player is still alive)
    PlayerHealth player;

    void Start()
    {
        // Find the player’s health component in the scene
        player = FindFirstObjectByType<PlayerHealth>();

        // Start the spawning loop
        StartCoroutine(SpawnRoutine());
    }

    // Coroutine that spawns robots at regular intervals
    IEnumerator SpawnRoutine()
    {
        // Keep spawning as long as the player exists (not destroyed)
        while (player)
        {
            // Spawn a robot at the spawn point position, facing the same rotation as the gate
            Instantiate(robotPrefab, spawnPoint.position, transform.rotation);

            // Wait for the next spawn cycle
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
