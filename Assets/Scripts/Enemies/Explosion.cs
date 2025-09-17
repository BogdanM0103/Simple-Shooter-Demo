using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Radius of the explosion effect (how far it reaches)
    [SerializeField] float radius = 1.5f;

    // Amount of damage dealt to the player if within the explosion radius
    [SerializeField] int damage = 3;

    void Start()
    {
        // Trigger explosion as soon as this object is spawned
        Explode();
    }

    void OnDrawGizmos()
    {
        // Draw a red wire sphere in the editor to visualize explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Handles explosion damage
    void Explode()
    {
        // Get all colliders within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the collider has a PlayerHealth component
            PlayerHealth playerhealth = hitCollider.GetComponent<PlayerHealth>();

            if (!playerhealth) continue;

            // Apply damage to the player
            playerhealth.TakeDamage(damage);

            // Stop after damaging the first player (in case of multiple colliders)
            break;
        }
    }
}
