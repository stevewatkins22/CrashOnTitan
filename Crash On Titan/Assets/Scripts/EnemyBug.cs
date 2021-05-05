using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBug : MonoBehaviour
{
    public int health = 20; // Set the health of the bug to 20
    public int value = 15; // Set the value that is added to the players score
    public int damage = 20; // set the amount of damage to bug does

    public float movementSpeed = 1.25f; // Set the movement speed of the bug

    public bool movingLeft = true; // Is the big moving left

    public GameObject player; // Reference to the player

    public Transform detectGround; // Detect the ground

    public GameManager gm; // reference to teh game master


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // Find the game object with the game manager component
    }

    private void Update()
    {
        transform.Translate(Vector2.right * -movementSpeed * Time.deltaTime); // Move the enemy to the left (Default facing direction is left)

        RaycastHit2D groundInfo = Physics2D.Raycast(detectGround.position, Vector2.down, 1f); // cast a ray from the detect ground game object downwards to check if there is ground
        if(groundInfo.collider == false) // If the ray does not detect a collider
        {
           if(movingLeft == true) // If the bug is moving left
            {
                Flip(); // Flip 180 degress on the y axis
                movingLeft = false; // set moving left to false
            }
           else // Otherwise
            {
                Flip(); // Flip back
                movingLeft = true; // moving left is set to true
            }
        }
    }

    public void TakeDamage(int damage) // Take damage from the player
    {
        health -= damage; // Minus the damage dealt from the bugs health

        if (health <= 0) // if health is less than 0
        {
            Die(); // Destroy game object
            AddScore(); // Add to players score
        }
    }

    void Die() // Called when health is less than 0
    {
        Destroy(gameObject); // Destroy this game object
    }

    void AddScore() // Add to the player score
    {
       gm.AddScore(value);
    }

    private void Flip() // Rotate the sprite 180 degress in the y axis
    {
        transform.Rotate(0, 180, 0);
    }

    void OnTriggerEnter2D(Collider2D col) // Upon collision
    {

        PlayerController player = col.GetComponent<PlayerController>(); // If the collsion is with the player
        if (player != null)
        {
            player.TakeDamage(damage); // Deal damage to the player
            Die(); // Die
        }
    }
}
