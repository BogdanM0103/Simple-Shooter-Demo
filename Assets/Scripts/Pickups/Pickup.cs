using UnityEngine;

// Base class for all pickups (e.g., ammo, weapons, health)
// Defines shared behavior like rotation and player interaction
public abstract class Pickup : MonoBehaviour
{
    // Speed at which the pickup rotates (for visual effect)
    [SerializeField] float rotationSpeed = 100f;

    // Tag used to identify the player object
    const string PLAYER_STRING = "Player";

    void Update()
    {
        // Continuously rotate the pickup around its Y-axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag(PLAYER_STRING))
        {
            // Get the ActiveWeapon component from the player (child object)
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();

            // Call the pickup effect (defined in child classes)
            OnPickup(activeWeapon);

            // Destroy the pickup object after being collected
            Destroy(this.gameObject);
        }
    }

    // Abstract method → must be implemented by subclasses (e.g., AmmoPickup, WeaponPickup)
    // Defines what happens when the player picks this up
    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
