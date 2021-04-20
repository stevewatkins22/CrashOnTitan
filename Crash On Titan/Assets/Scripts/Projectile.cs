using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5;
    public int damage = 10;

    public Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        rbody.velocity = transform.right * -speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);

    }
}

