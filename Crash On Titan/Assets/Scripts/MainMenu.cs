using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private int sceneToContinue; // Scene index number for continueing game

    public int setHealth = 100; //Initial ammount of health for the player
    public int setLives = 3; // Initial number of lives the player has
    public int setJumps = 2; // Initial number of jumps the player has

    public GameObject SettingMenuUI; // Used to hide or show the gameobject

    private void Start()
    {
        SettingMenuUI.SetActive(false); // Set the settings UI menu to false by default
    }

    public void Continue() // Continue game from last scene loaded
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene"); // Set the scene to load to the saved index number
        if(sceneToContinue!=0) // if the Scene index is not 0
        {
            SceneManager.LoadScene(sceneToContinue); // Load the scene 
        }
        else
        {
            New(); // Start a new game if the current scene index is 0 (Main menu scene)
        }
    }

    public void New() // Start a new game
    {
        PlayerPrefs.DeleteKey("Score"); // Delete previously saved score
        PlayerPrefs.DeleteKey("Retry"); // Delete previous retry (Used for game over)
        PlayerPrefs.SetInt("Lives", setLives); // Set Lives
       PlayerPrefs.SetInt("Health", setHealth); // Set the players max health
        PlayerPrefs.SetInt("Jumps", setJumps); // Set Jumps
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); // Load the next scene within the build index (Level_1)
    }

    public void NewGame() // This is used from within the Game over UI, to start a new game without going to the main menu first
    {
        PlayerPrefs.DeleteKey("Score"); // Delete previously saved score
        PlayerPrefs.DeleteKey("Retry"); // Delete previous retry
        PlayerPrefs.SetInt("Lives", setLives); // Set Lives
        PlayerPrefs.SetInt("Health", setHealth); // Set the players max health
        PlayerPrefs.SetInt("Jumps", setJumps); // Set Jumps
        SceneManager.LoadScene("Level_1"); // Load level one from any scene
    }
    
    public void SettingsOpen() // Open the settings menu
    {
        SettingMenuUI.SetActive(true); //  Show the settings menu
    }

    public void SettingsClose() // Close the settings menu
    {
        SettingMenuUI.SetActive(false); // Hide the settings menu
    }
}
