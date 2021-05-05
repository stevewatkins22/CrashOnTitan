using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5; // speed the projectile will travel at
    public int damage = 10; // Damage the projectile will do if it comes into contact with the player

    public Rigidbody2D rbody; // Rigidbody of the projectile to simulate gravity and apply movement
    
    // Start is called before the first frame update
    void Start()
    {
        rbody.velocity = transform.right * -speed; // the object will spawn out of the enemy from a location placed to the left, so apply velocity to the left
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController player = hitInfo.GetComponent<PlayerController>(); // find the player

        if (player != null) // if the projectile comes into contact with the player
        {
            player.TakeDamage(damage); // apply damage to the player
            Destroy(gameObject); // delete the projectile
        }
        else if (hitInfo.gameObject.CompareTag("Coins")) { } // Ignore collision with coins
        else
        {
            Destroy(gameObject); // if the projectile comes into contact with any other collider, such as a wall, delete it
        }

    }
}

