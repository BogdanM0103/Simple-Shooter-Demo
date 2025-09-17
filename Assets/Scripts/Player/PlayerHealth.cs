using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Starting health for the player (adjustable in Inspector, between 1 and 10)
    [Range(1, 10)]
    [SerializeField] int startingHealth = 5;

    // Virtual camera that activates on player death (e.g., cinematic death view)
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;

    // Reference to the weapon camera (detached when the player dies)
    [SerializeField] Transform weaponCamera;

    // UI shield bars (used to visually represent current health)
    [SerializeField] Image[] shieldBars;

    // UI container for the Game Over screen
    [SerializeField] GameObject gameOverContainer;

    // Tracks the player’s current health
    int currentHealth;

    // Priority assigned to the death camera to make it override the main camera on death
    int gameOverVirtualCameraPriority = 20;

    void Awake()
    {
        // Initialize health to the starting value
        currentHealth = startingHealth;

        // Update UI to match current health
        AdjustShieldUI();
    }

    // Called when the player takes damage
    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Reduce health
        AdjustShieldUI();        // Update UI

        // If health reaches 0 or less → trigger game over
        if (currentHealth <= 0)
        {
            PlayerGameOver();
        }
    }

    // Handles player death and game over logic
    void PlayerGameOver()
    {
        // Detach weapon camera from player so it doesn’t get destroyed
        weaponCamera.parent = null;

        // Switch to death camera by increasing its priority
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;

        // Show the Game Over screen
        gameOverContainer.SetActive(true);

        // Unlock and show cursor (so player can interact with menu)
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);

        // Destroy the player object
        Destroy(this.gameObject);
    }

    // Updates the shield UI to reflect current health
    void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                // Show shield bar if it's within the player's health
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                // Hide shield bar if health has dropped below this index
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }
}
