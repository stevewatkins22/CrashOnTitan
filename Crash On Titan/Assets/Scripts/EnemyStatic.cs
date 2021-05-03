using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    public int health = 50;
    public int value = 50;

    public bool playerFound;
    public bool ShootAtPlayer;
    public bool isFacingLeft;

    public float FindPlayerRadius;
    public float ShootAtPlayerRadius;
    public float waitBetweenShots;

    public GameObject Player;
    public GameObject projectile;

    public Transform LookAt;
    public Transform ShootAt;
    public Transform ProjectileExit;

    public LayerMask whatIsPlayer;

    public GameManager gm;

    private float shotCounter;

    void Start()
    {
        isFacingLeft = true;

        shotCounter = waitBetweenShots;

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        CheckSurroundings();
        CheckFacingDirection();

        shotCounter -= Time.deltaTime;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
            AddScore();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void AddScore()
    {
        gm.AddScore(value);
    }

    private void CheckSurroundings()
    {
      playerFound = Physics2D.OverlapCircle(LookAt.position, FindPlayerRadius, whatIsPlayer);
      ShootAtPlayer = Physics2D.OverlapCircle(ShootAt.position, ShootAtPlayerRadius, whatIsPlayer);
    }

    private void CheckFacingDirection()
    {
        if (playerFound)
        {
                Flip();
        }


        if(ShootAtPlayer && shotCounter < 0)
        {
            ShootPlayer();
            shotCounter = waitBetweenShots;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(LookAt.position, FindPlayerRadius);

        Gizmos.DrawWireSphere(ShootAt.position, ShootAtPlayerRadius);
        
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    private void ShootPlayer()
    {
        Instantiate(projectile, ProjectileExit.position, ProjectileExit.rotation);
    }
}

