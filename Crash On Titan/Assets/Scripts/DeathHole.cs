using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHole : MonoBehaviour
{
    public int damage = 100; // damage amount to deal

    void OnTriggerEnter2D(Collider2D other) // When a collider passes through collider attached to the game object this script is attached to
    {
        PlayerController player = other.GetComponent<PlayerController>(); // Get the player
        if (player != null) // if the player pass through the collider
        {
            player.TakeDamage(damage); // deal damage to the player
        }

    }
}
