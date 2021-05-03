using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8;
    public int damage = 10;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyStatic enemy = hitInfo.GetComponent<EnemyStatic>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(hitInfo.gameObject.CompareTag("Coins")){}
        else
        {
            Destroy(gameObject);
        }
        

    }
}

