using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Reference to the particle system used for muzzle flash effect
    [SerializeField] ParticleSystem muzzleFlash;

    // Defines which layers the weapon can interact with (e.g., enemies, environment)
    [SerializeField] LayerMask interactionLayers;

    // Reference to Cinemachine impulse source for adding camera shake when shooting
    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        // Get the CinemachineImpulseSource component attached to this object
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Called whenever the weapon is fired
    public void Shoot(WeaponSO weaponSO)
    {
        // Play muzzle flash effect
        muzzleFlash.Play();

        // Trigger camera shake/impulse effect
        impulseSource.GenerateImpulse();

        RaycastHit hit;

        // Cast a ray from the center of the camera forward into the scene
        if (Physics.Raycast(
            Camera.main.transform.position,               // Ray starts at the camera position
            Camera.main.transform.forward,               // Direction: where the camera is looking
            out hit,                                     // Store hit information
            Mathf.Infinity,                              // Infinite ray length
            interactionLayers,                           // Only interact with specified layers
            QueryTriggerInteraction.Ignore))             // Ignore triggers
        {
            // Spawn a hit visual effect (like sparks or blood) at the impact point
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);

            // Check if the hit object (or its parent) has an EnemyHealth component
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();

            // If an enemy was hit, apply damage defined in the weapon scriptable object
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
