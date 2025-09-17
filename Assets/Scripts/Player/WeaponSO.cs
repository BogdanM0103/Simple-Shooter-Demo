using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    // Prefab of the weapon model (what will be spawned/attached to the player)
    public GameObject weaponPrefab;

    // Amount of damage this weapon deals per shot
    public int Damage = 1;

    // Time (in seconds) between shots – controls fire rate
    public float FireRate = .5f;

    // Prefab for visual effects when the weapon hits something (e.g., sparks, blood, impact particles)
    public GameObject HitVFXPrefab;

    // If true, the weapon can continuously fire when the shoot button is held
    public bool isAutomatic = false;

    // If true, this weapon supports zoom/aim down sights
    public bool CanZoom = false;

    // How much the camera zooms in when aiming (field of view reduction)
    public float ZoomAmount = 10f;

    // Speed of zooming in/out transition
    public float ZoomRotationSpeed = .3f;

    // Maximum number of bullets in a single magazine/clip
    public int MagazineSize = 12;
}
