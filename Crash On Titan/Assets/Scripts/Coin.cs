using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameManager gm; // GameManager game object

    public int value = 20; // value to add to score 

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // find the game manager object
    }

    private void OnTriggerEnter2D(Collider2D other) // upon entering collider set to trigger
    {
        if(other.gameObject.CompareTag("Player")) // if the object is the player
        {
            gm.AddScore(value); // Add the value to the score
        }
        if(other.gameObject.CompareTag("ChildOfPlayer")) // The the collider belongs to a child of the player
        {
            gm.AddScore(value); // Add the value to the score
        }
    }
}
