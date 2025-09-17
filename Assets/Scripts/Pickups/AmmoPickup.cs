using UnityEngine;

// Pickup that adds ammo to the player's current weapon
public class AmmoPickup : Pickup
{
    // Amount of ammo this pickup grants to the player
    [SerializeField] int ammoAmount = 100;

    // Called when the player collects this pickup
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        // Increase the player's current ammo by the defined amount
        activeWeapon.AdjustAmmo(ammoAmount);
    }
}
