using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Visual effect prefab spawned when the enemy dies (e.g., explosion)
    [SerializeField] GameObject robotExplosionVFX;

    // Enemy's starting health value
    [SerializeField] int startingHealth = 3;

    // Tracks the current health of this enemy
    int currentHealth;

    // Reference to the GameManager for enemy count tracking
    GameManager gameManager;

    void Awake()
    {
        // Initialize health when the enemy is created
        currentHealth = startingHealth;
    }

    void Start()
    {
        // Get a reference to the GameManager in the scene
        gameManager = FindFirstObjectByType<GameManager>();

        // Register this enemy with the GameManager (increase total enemies left)
        gameManager.AdjustEnemiesLeft(1);
    }

    // Called when the enemy takes damage
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // If health is depleted, notify GameManager and self-destruct
        if (currentHealth <= 0)
        {
            gameManager.AdjustEnemiesLeft(-1);
            SelfDestruct();
        }
    }

    // Destroy this enemy and spawn explosion effect
    public void SelfDestruct()
    {
        // Spawn explosion at enemy's position
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);

        // Remove enemy from the scene
        Destroy(this.gameObject);
    }
}
