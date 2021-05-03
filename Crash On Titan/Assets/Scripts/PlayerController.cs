using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private Animator anim;
    private GameObject[] players;

    private float movementInputDirection;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canJump;
    private bool isJumping;

    public float movementSpeed = 10;
    public float jumpForce = 16;
    public float groundCheckRadius;

    public int lives;
    public int maxLives = 3;

    public int maxHealth = 100;
    public int currentHealth;

    public int numberOfJumps;
    public int numberOfJumpsLeft;
    public int maxNumberOfJumps = 3;

    public Transform groundCheck;
    public HealthBar healthBar;
    public LayerMask whatIsGround;

    public GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        lives = PlayerPrefs.GetInt("Lives");
        numberOfJumps = PlayerPrefs.GetInt("Jumps");
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
       //lives = maxLives;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        numberOfJumpsLeft = numberOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckSurroundings();
        CheckIfCanJump();
        CheckHealth();
    }

    private void OnLevelWasLoaded(int level)
    {
        FindStartPos();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
 
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            numberOfJumpsLeft = numberOfJumps;
            isJumping = false;
        }
        
        if(numberOfJumpsLeft > 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
    
    else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if(rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isJumping", isJumping);
    }

    private void CheckInput()
    {
        movementInputDirection = CrossPlatformInputManager.GetAxis("Horizontal");
        if(CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void ApplyMovement()
    {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Jump()
    {
        if (canJump && numberOfJumpsLeft > 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            numberOfJumpsLeft--;
            isJumping = true;
        }
        else if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            numberOfJumpsLeft--;
            isJumping = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        { Destroy(other.gameObject); }

        if (other.gameObject.CompareTag("Upgrade"))
        {
            gm.useBtn.SetActive(true);
        }
        else
        {
            gm.useBtn.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }

    public void Heal() //Useable at upgrade stations to heal player to max health
    {

        if (currentHealth < maxHealth && gm.score > 500)
        { 
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gm.score -= 500;
        gm.UpdateScore();

        }
    }

    public void AddLife() //Regain a lost life for 1500 score
    {
        if (lives < maxLives && gm.score > 1500)
        {
            lives++;
            PlayerPrefs.SetInt("Lives", lives);
            gm.score -= 1500;
            gm.UpdateScore();
            gm.UpdateLives();
        }
    }

    public void UnlockTripleJump() //Add an addition jump to a total of three
    {
        if (numberOfJumps < maxNumberOfJumps && gm.score > 1000)
        {
            numberOfJumps = maxNumberOfJumps;
            PlayerPrefs.SetInt("Jumps", numberOfJumps);
            gm.score -= 1000;
            gm.UpdateScore();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void CheckHealth()
    {
        gm.UpdateLives();
        if (currentHealth <= 0 && lives > 1)
        {
            lives--;
            PlayerPrefs.SetInt("Lives", lives);
            gm.UpdateLives();
            Respawn();
        }
        else if (currentHealth <= 0)
        {
            Die();
            lives--;
            PlayerPrefs.SetInt("Lives", lives);
            gm.UpdateLives();
            gm.GameOver();
        }
    }

    void Respawn()
        {
        gm.Respawn();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        }

    void FindStartPos()
    {
        transform.position = GameObject.FindWithTag("Respawn/Start").transform.position;
    }
}
