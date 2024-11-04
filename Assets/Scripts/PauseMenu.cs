using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject pauseMenuUI; // The PausePanel GameObject
    private bool isPaused = false;

    void Start()
    {
        // Ensure the pause menu is hidden initially
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Toggle pause when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false); // Hide the pause menu
    }

    public void GoToHome()
    {
        // Return to the character select scene
        Time.timeScale = 1f; // Reset time scale before loading the new scene
        SceneManager.LoadScene(0); // Replace "SelectCharScene" with the name of your character selection scene
    }

    public void QuitGame()
    {
        // Optional: Quit the game (works only in a build)
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
