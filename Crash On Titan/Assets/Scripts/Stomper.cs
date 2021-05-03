using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    public int damage;
    public float bounceForce;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bug"))
        {
            other.gameObject.GetComponent<EnemyBug>().TakeDamage(damage);
            rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);
        }
    }

}
