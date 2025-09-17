using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // UI text element showing how many enemies are left
    [SerializeField] TMP_Text enemiesLeftText;

    // UI element that displays "You Win" when the player clears the level
    [SerializeField] GameObject youWinText;

    // Tracks how many enemies are currently alive
    int enemiesLeft = 0;

    // Constant label for the enemies left UI
    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    // Adjust the number of enemies left (positive = add enemies, negative = remove enemies)
    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;

        // Update the UI text
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        // If no enemies remain, show the win screen
        if (enemiesLeft <= 0)
        {
            youWinText.SetActive(true);
        }
    }

    // Reloads the current scene (used by a Restart button in the UI)
    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    // Quits the game (works in a built application, but not in the Unity editor)
    public void QuitButton()
    {
        Debug.LogWarning("Does not work in the Unity Editor!  You silly goose!");
        Application.Quit();
    }
}
