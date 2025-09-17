using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Prefab of the projectile the turret fires
    [SerializeField] GameObject projectilePrefab;

    // Part of the turret that rotates to face the player
    [SerializeField] Transform turretHead;

    // Target point on the player to aim at (e.g., chest or head transform)
    [SerializeField] Transform playerTargetPoint;

    // Where projectiles are spawned from (usually at the turret’s muzzle)
    [SerializeField] Transform projectileSpawnPoint;

    // Time (in seconds) between shots
    [SerializeField] float fireRate = 2f;

    // How much damage each projectile deals to the player
    [SerializeField] int damage = 2;

    // Reference to the player's health component
    PlayerHealth player;

    void Start()
    {
        // Find the player's health script in the scene
        player = FindFirstObjectByType<PlayerHealth>();

        // Start firing projectiles on a loop
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        // Continuously rotate the turret head to aim at the player
        if (playerTargetPoint)
        {
            turretHead.LookAt(playerTargetPoint);
        }
    }

    // Coroutine that handles firing at intervals
    IEnumerator FireRoutine()
    {
        // Keep firing as long as the player exists
        while (player)
        {
            // Wait for the defined fire rate
            yield return new WaitForSeconds(fireRate);

            // Spawn a projectile at the spawn point
            Projectile newProjectile = Instantiate(
                projectilePrefab,
                projectileSpawnPoint.position,
                Quaternion.identity
            ).GetComponent<Projectile>();

            // Aim projectile towards player
            newProjectile.transform.LookAt(playerTargetPoint);

            // Initialize projectile with damage value
            newProjectile.Init(damage);
        }
    }
}
