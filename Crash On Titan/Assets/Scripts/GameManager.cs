using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score; // Player score
    public int retry; // Retry attempt
    public int retryCount; // Retry attempt count
    public bool retryed; // Bool if attempted retry

    public Transform respawnPoint; //  Set the players respawn point
    public Transform player; // The player

    public Text scoreText; // Text to display score
    public Text livesText; // Text to display lives

    public PlayerController pc; // Reference to the PlayerController script

    public GameObject upgradeMenu; // Reference to the upgrade menu ui
    public GameObject gameOverUI; // reference to the game over ui
    public GameObject useBtn; // reference to the use button
    public GameObject retryBtn; // reference to the retry button

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false); // Hide the game over ui on start
        upgradeMenu.SetActive(false); // hide the upgrade menu on start
        useBtn.SetActive(false); // hide the use button on start
        Time.timeScale = 1f; // set game speed to 1 (Essentially start the game)
        retry = PlayerPrefs.GetInt("Retry"); // set retry to either 1 or 0
        if(retry != 1) // if retry is 0 - This is used to determine if the retry button will appear
        {
            retryed = false; // Set the bool to false
        }
        else
        {
            retryed = true; // set the bool to true
        }
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
        gameOverUI.SetActive(true); // Display the game over ui
        if (retryed) // If the player has retryed 
        {
            retryBtn.SetActive(false); // do not show the retry button
        }
        else; // else show the retry button
        Time.timeScale = 0f; // pause the game

    }

    public void Retry() // Retry function will reload the current scene and give the player 1 additional life
    {
        PlayerPrefs.SetInt("Score", score); // Set the score to what they have
        pc.lives++; // Add one life
        retryCount++; // Add one to player count
        PlayerPrefs.SetInt("Lives", pc.lives); // Save the number of lives
        UpdateLives(); // Update the lives text
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reload the current scene
        PlayerPrefs.SetInt("Retry", retryCount); // set retry to the retry count
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
