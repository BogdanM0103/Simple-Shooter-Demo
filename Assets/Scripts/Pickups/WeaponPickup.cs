using UnityEngine;

// Pickup that equips the player with a new weapon
public class WeaponPickup : Pickup
{
    // Reference to the weapon ScriptableObject (defines stats, prefab, etc.)
    [SerializeField] WeaponSO weaponSO;

    // Called when the player collects this pickup
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        // Switch the player's current weapon to the new one
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
