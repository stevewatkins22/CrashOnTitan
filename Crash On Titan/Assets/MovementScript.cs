using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MovementScript : MonoBehaviour
{
    public float speed;
    public float jumpForce = 200f;
    public bool b_isGrounded;
    public bool b_FacingRight;

    public Joystick joystick;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //b_isGrounded = true;
        rigidbody = GetComponent<Rigidbody2D>();
        b_FacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Vector2.zero;

        dir.x = joystick.Horizontal();
        dir.y = 0;

        if (dir.magnitude > 1)
            dir.Normalize();

        if(dir.x > 0 && !b_FacingRight)
        {
            flip();
            rigidbody.AddForce(dir * speed);
        }
        else
        {
            rigidbody.AddForce(dir * speed);
        }

        if (dir.x < 0 && b_FacingRight)
        {
            flip();
            rigidbody.AddForce(dir * speed);
        }
        else
        {
            rigidbody.AddForce(dir * speed);
        }

    

       // rigidbody.AddForce(dir * speed);

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float Jump = Input.GetAxis("Jump");

        //Vector2 movement = new Vector2(moveHorizontal, 0);

        //rigidbody.AddForce(movement * speed);

        //if (Input.GetButtonDown("Jump"))
        //     jump();

        //if (Input.GetAxis("Horizontal") < 0)
        //{
        //    if (b_FacingRight == true)
        //    {
        //        flip();
        //        moveLeft = new Vector2(moveHorizontal, 0);
        //        rigidbody.AddForce(moveLeft * speed);
        //    }
        //    else
        //    {
        //        moveLeft = new Vector2(moveHorizontal, 0);
        //        rigidbody.AddForce(moveLeft * speed);
        //    }
        //}

        //if (Input.GetAxis("Horizontal") > 0)
        //{
        //    if (b_FacingRight == false)
        //    {
        //        flip();
        //        moveRight = new Vector2(moveHorizontal, 0);
        //        rigidbody.AddForce(moveRight * speed);
        //    }
        //    else
        //    {
        //        moveRight = new Vector2(moveHorizontal, 0);
        //        rigidbody.AddForce(moveRight * speed);
        //    }
        //}

        //if(joystick.Horizontal() > 0)
        //{
        //    if (b_FacingRight == true)
        //    {
        //        flip();
        //        moveLeft = new Vector2(moveHorizontal, 0);
        //        rigidbody.AddForce(moveLeft * speed);
        //    }
        //    else
        //    {
        //        moveLeft = new Vector2(moveHorizontal, 0);
        //        rigidbody.AddForce(moveLeft * speed);
        //    }
        //}

        //if (joystick.Horizontal() < 0)
        //{
        //    if (b_FacingRight == false)
        //    {
        //        flip();
        //        moveRight = new Vector2(moveHorizontal, 0);
        //        rigidbody.AddForce(moveRight * speed);
        //    }
        //    else
        //    {
        //        moveRight = new Vector2(moveHorizontal, 0);
        //        rigidbody.AddForce(moveRight * speed);
        //    }
        //}

    }

    public void jump()
    {
        if(b_isGrounded == true)
        {
            rigidbody.AddForce(new Vector2(0f, jumpForce));
            b_isGrounded = false;
        }
        else
        {
            b_isGrounded = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        b_isGrounded = true;
    }

    public void flip()
    {
        if(!b_FacingRight)
        {
            b_FacingRight = true;
        }
        else
        {
            b_FacingRight = false; 
        }
        transform.Rotate(0f, 180f, 0f);
    }
}
