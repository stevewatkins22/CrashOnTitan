using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition; // animation controller for the transition

    public GameManager gm; // Reference to the GameMaster
    public PlayerController pc; // Reference to the PlayerController

    public float transitionTime = 1; // time taken to complete animation

    public int value = 100; // value to add to the score

    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // Find the game object with the player controller component
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // find the game object with the game manager component
    }

    public void LoadNextLevel() // Load the next level
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); // Load the next scene within the build index
    }

    IEnumerator LoadLevel(int levelIndex) // IEnumerator for function to allow time to pass between transition and loading scene
    {
        transition.SetTrigger("Start"); // Start the animation with trigger

        PlayerPrefs.SetInt("Score", gm.score); // Save the players score
        PlayerPrefs.SetInt("Health", pc.currentHealth); // Set the players health
        PlayerPrefs.SetInt("Lives", pc.lives); // Save the players lives
        PlayerPrefs.SetInt("Jumps", pc.numberOfJumps); // Save the players number of jumps

        yield return new WaitForSeconds(transitionTime); // Wait for transition time

        SceneManager.LoadScene(levelIndex); // Load the next level in the build index
    }

    private void AddScore() // Add to the player score
    {
        gm.AddScore(value); // call the function within the game manager and add the value
    }


    private void OnTriggerEnter2D(Collider2D collision) // Upon triggering a collision
    {
        GameObject collisionGameObject = collision.gameObject; //Game object that triggers the collision

        if (collisionGameObject.name == "Player") // If it is the player
        {
            AddScore(); // Add to score 
            LoadNextLevel(); // Load next level
        }
        else // if it is not the player
        {
            Debug.Log("Not a player"); // Do nothing
        }
    }
}
