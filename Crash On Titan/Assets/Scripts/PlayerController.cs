using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; // Reference to the rigidbody of the player
    private Animator anim; // Reference to the animator
    
    private float movementInputDirection; // Float to apply movement direction - -1 for moving left +1 for moving right

    private bool isFacingRight = true; // Is the player facing towards the right - Set to true by default
    private bool isWalking; // Is the player moving - Used for the movement animation
    private bool isGrounded; // Is the player grounded - Used to determine if the player is touching the ground
    private bool canJump; // Can the player jump - Used to determine if the player can jump
    private bool isJumping; // Is the player currently jumping

    public float movementSpeed = 4f; // Set the players movement speed
    public float jumpForce = 2.5f; // Set the players jump speed/force
    public float groundCheckRadius; //Radius used to determine if the player is grounded - Set within editor to 0.15 (A small circle around the players feet

    public int lives; // Current number of lives
    public int maxLives = 3; // Maximum number of lives the player can have

    public int maxHealth = 100; // Maximum amount of health the player can have
    public int currentHealth; // Player current health

    public int numberOfJumps; //Number of jumps the player has
    public int numberOfJumpsLeft; //How many times the player can jump before needing to be grounded again
    public int maxNumberOfJumps = 3; // Maximum number of jumps the player can have at a time

    public Transform groundCheck; // Location fo the ground check gameobject (Placed at the feet of the player)
    public HealthBar healthBar; // The players health bar in the UI
    public LayerMask whatIsGround; // Determine what is ground for the player - this is set to the ground layer (The platforms tilemap is also set to this layer)

    public GameManager gm; // Reference to the game master
    
    // Start is called before the first frame update
    void Start()
    {
        lives = PlayerPrefs.GetInt("Lives"); // Get the number of lives the player has from playerprefs
        numberOfJumps = PlayerPrefs.GetInt("Jumps"); // Get the number of jumps the player has from player prefs
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // Find the game object with the game manager component
        currentHealth = PlayerPrefs.GetInt("Health"); // Get the current health from the player
        healthBar.setHealth(currentHealth); // Set he health bar to display the players current health
        rb = GetComponent<Rigidbody2D>(); // Get the rigisbody from the player
        anim = GetComponent<Animator>(); // Get the animator from the player
        numberOfJumpsLeft = numberOfJumps; // Set the ammount of jumps the player has left to the ammount of jumps the player has
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput(); // Check the users input
        CheckMovementDirection(); // Check which direction the player is moving in
        UpdateAnimations(); // Update the players animations
        CheckSurroundings(); // Check if the player is touching the ground
        CheckIfCanJump(); // Check if the player can jump
        CheckHealth(); // Check the players health
    }

    private void FixedUpdate() // Update within sync of the physics engine
    {
        ApplyMovement(); // Apply movement to the player
    }

    private void CheckSurroundings() // Check if the player is touching the ground
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); // Set is grounded to true, if the radius of the ground check attached to the feet of the player comes into contact with the ground layer
 
    }

    private void CheckIfCanJump() // Check if the player can jump
    {
        if (isGrounded && rb.velocity.y <= 0) // if the player is grounded and the player is not going up
        {
            numberOfJumpsLeft = numberOfJumps; // Number of jumps left is equal to the number of jumps available
            isJumping = false; // is jumping is set to false
        }
        
        if(numberOfJumpsLeft > 0) // If the number of jumps left is more than 0
        {
            canJump = true; // The player can jump
        }
        else // if it is equal to 0
        {
            canJump = false; // The player can not jump
        }
    }

    private void CheckMovementDirection() // Check the move ment direction
    {
        if(isFacingRight && movementInputDirection < 0) // if the player is facing right and the player is moving left
        {
            Flip(); // Flip the player
        }
    
    else if(!isFacingRight && movementInputDirection > 0) // if the player is not facing right and the player is moving right
        {
            Flip(); // Flip the player
        }

        if(rb.velocity.x != 0) // If the players velocity is not 0 then the player is walking
        {
            isWalking = true; // Set is walking to true
        }
        else // If the players velocity is 0, the player is not walking
        {
            isWalking = false; // Set is walking to false
        }
    }

    private void UpdateAnimations() // Update the players animations
    {
        anim.SetBool("isWalking", isWalking); // If the player is walking play the walking animation
        anim.SetBool("isJumping", isJumping); // if the player is jumping, play the jumping animation
    }

    private void CheckInput() // Check the players input
    {
        movementInputDirection = CrossPlatformInputManager.GetAxis("Horizontal"); // Using the cross platform input manager available on the unity asset store - get the horizontal movement
        if(CrossPlatformInputManager.GetButtonDown("Jump")) // Using the cross platform input manager available on the unity asset store - Get the jump button
        {
            Jump(); // Jump
        }
    }

    private void ApplyMovement() // Apply movement to the player
    {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y); // Apply velocity to the players rigidbody, multiplying the movement speed by the input direction
        
    }

    private void Flip() // Flip the player sprite by 180 degress
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Jump() // Make the player jump
    {
        if (canJump && numberOfJumpsLeft > 1) // if the player can jump and the number of jumps left is greater than one
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply force to lift the player vertically
            numberOfJumpsLeft--; // Remove 1 jump
            isJumping = true; // Set is jumping to true for the aniamtor
        }
        else if(canJump) // if the player can jump and only has 1 jump left
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply force to lift the player vertically
            numberOfJumpsLeft--; // Remove 1 jump
            isJumping = true; // Set is jumping to true for the aniamtor
        }
    }

    private void OnDrawGizmos() // Draw the radius of the ground check in the editor window
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void OnTriggerEnter2D(Collider2D other) // Upon entering collision with a trigger
    {
        if (other.gameObject.CompareTag("Coins")) // Upon colliding with coins
        { Destroy(other.gameObject); } // Destroy the coin

        if (other.gameObject.CompareTag("Upgrade")) // Upon colliding with the upgrade machine
            gm.useBtn.SetActive(true); // Show the use button
   
    }

    private void OnTriggerExit2D(Collider2D other) // Upon exitting collision with a trigger
    {
        if (other.gameObject.CompareTag("Upgrade")) // Upon exiting the uograde machines collider
            gm.useBtn.SetActive(false); // Hide the use button
        
    }

    public void TakeDamage(int damage) // Apply damage to the player - This is called from other scripts, passing in the amount of damage done
    {
        currentHealth -= damage; // minus the amount of damage from the current health
        healthBar.setHealth(currentHealth); // Update the health bar
        PlayerPrefs.SetInt("Health", currentHealth); // Update the saved amount of health the player has
    }

    public void Heal() //Useable at upgrade stations to heal player to max health
    {

        if (currentHealth < maxHealth && gm.score > 250) //  if the players health is less than the maxhealth and they have at least 250 score available
        { 
        currentHealth = maxHealth; // Set the current health to the max health
        PlayerPrefs.SetInt("Health", currentHealth); // Update the players saved health
        healthBar.SetMaxHealth(maxHealth); // Update the health bar
        gm.score -= 250; // Remove 250 score from the player
        gm.UpdateScore(); // Update the players score
        }
    }

    public void AddLife() //Regain a lost life for 1500 score
    {
        if (lives < maxLives && gm.score > 750) //  if the players lives is less than the maxLives and they have at least 750 score available
        {
            lives++; // Increase player lives by 1
            PlayerPrefs.SetInt("Lives", lives); // Save the players lives
            gm.score -= 750; // remove score from player
            gm.UpdateScore(); // update score - text
            gm.UpdateLives(); // Update the players lives - text
        }
    }

    public void UnlockTripleJump() //Add an addition jump to a total of three
    {
        if (numberOfJumps < maxNumberOfJumps && gm.score > 500) //  if the players number of jumps is less than the max number of jumps and they have at least 500 score available
        {
            numberOfJumps = maxNumberOfJumps; // Set the number of jumps to the max number fo jumps
            PlayerPrefs.SetInt("Jumps", numberOfJumps); // Save the players number of jumps
            gm.score -= 500; // remove score from player
            gm.UpdateScore(); // update the score - text
        }
    }

    void Die() // If the player runs out of lifes and has 0 health, destroy
    {
        Destroy(gameObject);
    }

    void CheckHealth() // Check the players health
    {
        gm.UpdateLives(); // Update the players lives - text
        if (currentHealth <= 0 && lives > 1) // if the players health is equal or less than 0  and they have more than 1 life
        {
            lives--; // Remove one life
            PlayerPrefs.SetInt("Lives", lives); // Save the players lives
            gm.UpdateLives(); // Update the text
            Respawn(); // call the respawn function
        }
        else if (currentHealth <= 0) // if the do not have any lives and their health is equal or less than 0
        {
            Die(); // call the Die function
            lives--; // remove a life
            PlayerPrefs.SetInt("Lives", lives); // save the players lives
            gm.UpdateLives();// update the lives text
            gm.GameOver(); // call the game over function within the game master script
        }
    }

    void Respawn() // If the player can respawn
        {
        gm.Respawn(); // call the respawn functionwith the game master script
        currentHealth = maxHealth; // set the players health to the max health
        PlayerPrefs.SetInt("Health", currentHealth); // save the players health
        healthBar.SetMaxHealth(maxHealth); // update the health bar
        }

}
