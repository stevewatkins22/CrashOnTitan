using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public int score; // Player score

    public Transform respawnPoint; //  Set the players respawn point
    public Transform player; // The player

    public Text scoreText; // Text to display score
    public Text livesText; // Text to display lives

    public PlayerController pc; // Reference to the PlayerController script

    public GameObject upgradeMenu; // Reference to the upgrade menu ui
    public GameObject gameOverUI; // reference to the game over ui
    public GameObject useBtn; // reference to the use button

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false); // Hide the game over ui on start
        upgradeMenu.SetActive(false); // hide the upgrade menu on start
        useBtn.SetActive(false); // hide the use button on start
        Time.timeScale = 1f; // set game speed to 1 (Essentially start the game)
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // Find the game object with the componenet PlayerController

        UpdateLives(); // Update current lives
        UpdateScore(); // Update current score
    }

    private void OnLevelWasLoaded(int level) // Upon loading a level
    {
        Time.timeScale = 1f; // Start the game
        score = PlayerPrefs.GetInt("Score"); // get the players score that was saved
        livesText.text = pc.lives.ToString(); // Update the score text
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore(); // Update the test every frame
    }

    public void AddScore(int value) // USed to add to the players score - The value is added by the scripts that call the function
    {
        score += value; // Add the value to the score
        scoreText.text = "Score: " + score.ToString(); // Update the score text
    }

    public void Respawn() // Respawn the player
    {
        player.position = respawnPoint.position; // Changed the players postion to the position of the respawn point
    }
    
    public void UpdateScore() // Update the players score
    {
        scoreText.text = "Score: " + score.ToString(); // Update the score text
    }

    public void UpdateLives() // Update the players lives
    {
        livesText.text = "Lives: " + pc.lives.ToString(); // Update the livest
    }


    public void GameOver() // Called whe the player runs out of lives
    {
        gameOverUI.SetActive(true); // Show the game over UI
        Time.timeScale = 0f; // pause the game
    }

    public void UpgradeMenu() // Open the upgrade menu ui
    {
        upgradeMenu.SetActive(true); // show the menu
        Time.timeScale = 0f; // pause the game
    }

    public void Continue() // Continue the game and close the Ui
    {
        upgradeMenu.SetActive(false); // Hide the menu
        Time.timeScale = 1f; // Unpause the game
    }
}
