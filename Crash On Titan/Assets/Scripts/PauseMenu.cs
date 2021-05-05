using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private int currentSceneIndex; // reference to the current scene index


    public GameObject PauseMenuUI; // reference to the pause menu
    public GameObject SettingsMenuUI; // reference to the settings menu

    public GameManager gm; // reference to the game master
    public PlayerController pc; // reference to the player controller

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // find the game object with the game manager component
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // find the game object with the player controller component

        PauseMenuUI.SetActive(false); // Hide the pause menu 
        SettingsMenuUI.SetActive(false); // Hide the settings menu
    }

    public void Resume() // Resume the game and close the menu
    {
        PauseMenuUI.SetActive(false); // Hide the pause menu ui
        Time.timeScale = 1f; // Unpause the game
   
    }

    public void Pause() // Pause the game and open the menu
    {
        PauseMenuUI.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // pause the game 

    }

    public void SettingsOpen() // Open the settings menu
    { 
        SettingsMenuUI.SetActive(true); // Show the settings menu
    }

    public void SettingsClose() // Close the settings menu
    {
        SettingsMenuUI.SetActive(false); // Hide the settings menu
    }

    public void QuitGame() // return to the main menu from the pause menu
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // save the current scene index
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex); // Save the scene index so that it can be continued from the main menu
        PlayerPrefs.SetInt("Score", gm.score); // save the score
        PlayerPrefs.SetInt("Health", pc.currentHealth); // Save the players health
        PlayerPrefs.SetInt("Lives", pc.lives); // save the number of lifes
        PlayerPrefs.SetInt("Jumps", pc.maxNumberOfJumps); // save the number of jumps
        SceneManager.LoadScene("MainMenu"); // Load the main menu
    }

    public void LoadMain() // Return to the main menu from the game over ui
    {
        SceneManager.LoadScene("MainMenu"); // load the main menu
        PlayerPrefs.DeleteKey("SavedScene"); // delete the last saved scene index
        PlayerPrefs.DeleteKey("Score"); // delete the previous score
        PlayerPrefs.DeleteKey("Lives"); // delete the number of lives
        PlayerPrefs.DeleteKey("Jumps"); // delete the number fo jumps
        PlayerPrefs.DeleteKey("Health"); // delete previous players health
    }
}
