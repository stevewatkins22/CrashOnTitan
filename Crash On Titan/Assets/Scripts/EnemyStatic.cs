using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    public int health = 50; // Amount of health the enemy has
    public int value = 50; // Value to add to score upon killing

    public bool playerFound; // Has the player been found
    public bool ShootAtPlayer; // Can shoot at player

    public float FindPlayerRadius; // Radius to find the player
    public float ShootAtPlayerRadius; // Radius to shoot at the player
    public float waitBetweenShots; // Time to wait between shots

    public GameObject Player; // Reference to the player
    public GameObject projectile; // Reference to the projectile

    public Transform LookAt; // Position of the Look at game object
    public Transform ShootAt; // Position of the shoot at game object
    public Transform ProjectileExit; // Position of where the projectile will be spawned from

    public LayerMask whatIsPlayer; // Define what the player is (Player layer within the sorting layers)

    public GameManager gm; // reference to the game master

    private float shotCounter; // used to count down when to shoot

    void Start()
    {
        shotCounter = waitBetweenShots; // Set the shot counter to the time to wait between shooting

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // Find the game object with the game master componenet
    }


    void FixedUpdate() 
    {
        CheckSurroundings(); // Check the surroundings of the enemy
        CheckFacingDirection();  // Check if the player is behind 

        shotCounter -= Time.deltaTime; // Decrease the shot counter every second

    }

    public void TakeDamage(int damage) // Take damage from player projectile - damage is passed by the script that calls it
    {
        health -= damage; // Remove the damage amount from the health

        if(health <= 0) // if health becomes less than 0
        {
            Die(); // Call the Die function
            AddScore(); // call the AddScore function
        }
    }

    void Die() // Called when health is less than 0
    {
        Destroy(gameObject); // Destroy this gameobject
    }

    void AddScore() // Add score to the players score
    {
        gm.AddScore(value); 
    }

    private void CheckSurroundings() // Check the game object surroundings
    {
      playerFound = Physics2D.OverlapCircle(LookAt.position, FindPlayerRadius, whatIsPlayer); // Set player found to true, if the player enters the radius of the look at game object
      ShootAtPlayer = Physics2D.OverlapCircle(ShootAt.position, ShootAtPlayerRadius, whatIsPlayer); // Set shoot at player to true, if the player enters the radius of the Shoot at game object
    }

    private void CheckFacingDirection() // Check which direction the game object is facing
    {
        if (playerFound) // If the player is found within the radius of the look at game object - this is located behind the game object
        {
                Flip(); // Flip the game object and its children 180 degress in the Y axis
        }


        if(ShootAtPlayer && shotCounter < 0) // If the player is within the shoot at radius and the shot counter is 0 
        {
            ShootPlayer(); // Shoot a projectile
            shotCounter = waitBetweenShots; // Set the counter back to the time required to wait between shot
        }
    }

    private void OnDrawGizmos() // Draw the radii of the two game objects within the editor window, when in play mode
    {
        Gizmos.DrawWireSphere(LookAt.position, FindPlayerRadius);

        Gizmos.DrawWireSphere(ShootAt.position, ShootAtPlayerRadius);
        
    }

    private void Flip() // Flip the sprite 180 degress in the y axis
    {
        transform.Rotate(0, 180, 0);
    }

    private void ShootPlayer() // Spawn a projectile in the players direction
    {
        Instantiate(projectile, ProjectileExit.position, ProjectileExit.rotation); // Spawn the projectile at the previous set position
    }
}

