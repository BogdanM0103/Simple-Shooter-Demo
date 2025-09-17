using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Speed of the projectile (units per second)
    [SerializeField] float speed = 30f;

    // Visual effect prefab to spawn when the projectile hits something
    [SerializeField] GameObject projectileHitVFX;

    // Cached Rigidbody component for physics movement
    Rigidbody rb;

    // Damage this projectile will deal on hit
    int damage;

    void Awake()
    {
        // Cache the Rigidbody for controlling movement
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Give the projectile forward velocity when it spawns
        rb.linearVelocity = transform.forward * speed;
    }

    // Called by the turret (or other shooter) when spawning a projectile
    public void Init(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has PlayerHealth
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        // If yes, deal damage to the player
        playerHealth?.TakeDamage(damage);

        // Spawn a hit effect at the impact position
        Instantiate(projectileHitVFX, transform.position, Quaternion.identity);

        // Destroy the projectile after impact
        Destroy(this.gameObject);
    }
}
