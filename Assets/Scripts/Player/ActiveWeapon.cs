using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    // Weapon that the player starts with
    [SerializeField] WeaponSO startingWeapon;

    // Player follow camera (main gameplay camera)
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;

    // Separate weapon camera (used for rendering weapons)
    [SerializeField] Camera weaponCamera;

    // Vignette UI effect that appears when zooming
    [SerializeField] GameObject zoomVignette;

    // UI element to display current ammo
    [SerializeField] TMP_Text ammoText;

    // Currently equipped weapon scriptable object
    WeaponSO currentWeaponSO;

    // Components
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;

    // Reference to the current weapon object
    Weapon currentWeapon;

    // Animation trigger constant
    const string SHOOT_STRING = "Shoot";

    // Timers, defaults, and ammo tracking
    float timeSinceLastShot = 0f;
    float defaultFOV;               // Camera field of view before zoom
    float defaultRotationSpeed;     // Player rotation speed before zoom
    int currentAmmo;                // Current ammo in magazine

    void Awake()
    {
        // Get necessary components
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();

        // Store default camera FOV and rotation speed for resetting after zoom
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        // Equip the starting weapon
        SwitchWeapon(startingWeapon);

        // Initialize ammo to full magazine
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void Update()
    {
        HandleShoot(); // Manage firing input
        HandleZoom();  // Manage zoom input
    }

    // Adjusts ammo count (positive = add ammo, negative = consume ammo)
    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        // Prevent ammo from exceeding max magazine size
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        // Update UI text, padded with 2 digits (e.g., 05, 12)
        ammoText.text = currentAmmo.ToString("D2");
    }

    // Switches to a new weapon
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        // Destroy old weapon if one exists
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        // Instantiate new weapon prefab as child of this object
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;

        // Update weapon scriptable object reference
        this.currentWeaponSO = weaponSO;

        // Reset ammo to full magazine
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    // Handles shooting logic
    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        // Only continue if shoot input is pressed
        if (!starterAssetsInputs.shoot) return;

        // Check if weapon cooldown is over and ammo is available
        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            // Shoot weapon
            currentWeapon.Shoot(currentWeaponSO);

            // Play shooting animation
            animator.Play(SHOOT_STRING, 0, 0f);

            // Reset timer and reduce ammo
            timeSinceLastShot = 0f;
            AdjustAmmo(-1);
        }

        // For semi-automatic weapons, reset input after single shot
        if (!currentWeaponSO.isAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    // Handles zooming logic (aim down sights)
    void HandleZoom()
    {
        // If this weapon can’t zoom, skip
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            // Apply zoom FOV to both cameras
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;

            // Enable vignette overlay for effect
            zoomVignette.SetActive(true);

            // Slow down player rotation for finer aiming control
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            // Reset cameras to default FOV
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;

            // Disable vignette
            zoomVignette.SetActive(false);

            // Reset rotation speed
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
