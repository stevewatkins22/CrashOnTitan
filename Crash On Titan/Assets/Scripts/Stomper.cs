using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    public int damage = 50; // Damage to deal
    public float bounceForce; // Amount to bounce the player upwards - set in the unity inspector

    private Rigidbody2D rb2d; // Declare rigidbody

    // Start is called before the first frame update
    void Start()
    {
        rb2d = transform.parent.GetComponent<Rigidbody2D>(); // get the rigidbody of the parent entity - The player
    }

    private void OnTriggerEnter2D(Collider2D other) // On triggering the collider
    {
        if (other.gameObject.CompareTag("bug")) // the the collider belongs to the bug enemy, do the following, otherwise ignore
        {
            other.gameObject.GetComponent<EnemyBug>().TakeDamage(damage); // Apply damage to the enemy
            rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce); // Apply upwards velocity to the player
        }
    }

}
