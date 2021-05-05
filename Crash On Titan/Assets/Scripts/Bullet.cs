using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8; // Speed the bullet will travel at
    public int damage = 10; // damage amount the bullet will deal

    public Rigidbody2D rb; // rigidbosy of the bullet for physics and applying movement
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed; // Apply velocity to the bullet so it travels to the right 
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyStatic enemy = hitInfo.GetComponent<EnemyStatic>(); // Find a static enemy
        EnemyBug bug = hitInfo.GetComponent<EnemyBug>(); // Find a bug enemy
        if (enemy != null) // if collision with static enemy
        {
            enemy.TakeDamage(damage); // deal damage to the enemy
            Destroy(gameObject); // destroy the bullet
        }
        else if(bug != null) // if collision with a bug enemy
        {
            bug.TakeDamage(damage); // deal damage to the bug
            Destroy(gameObject); // destroy the bullter
        }
        else if(hitInfo.gameObject.CompareTag("Coins")){} // ignore object if a coin
        else if (hitInfo.gameObject.CompareTag("Upgrade")) { } // ignore object if it is the upgrade machine
        else
        {
            Destroy(gameObject); // destroy upon contact with any other collider, such as a wall
        }
        

    }
}

