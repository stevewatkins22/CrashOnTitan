using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBug : MonoBehaviour
{
    public int health = 20;
    public int value = 15;
    public int damage = 5;

    public float movementSpeed = 1;
    public float waitBetweenAttack;

    public bool movingLeft = true;

    public GameObject player;

    public Transform detectGround;

    public GameManager gm;

    private float attackCounter;

    private Rigidbody2D rb;

    private void Start()
    {
        attackCounter = waitBetweenAttack;

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * -movementSpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(detectGround.position, Vector2.down, 1f);
        if(groundInfo.collider == false)
        {
           if(movingLeft == true)
            {
                Flip();
                movingLeft = false;
            }
           else
            {
                Flip();
                movingLeft = true;
            }
        }
    }

    private void FixedUpdate()
    {
        attackCounter -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
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

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        PlayerController player = col.GetComponent<PlayerController>();
        if (player != null && attackCounter < 0)
        {
            player.TakeDamage(damage);
            attackCounter = waitBetweenAttack;
        }
    }
}
