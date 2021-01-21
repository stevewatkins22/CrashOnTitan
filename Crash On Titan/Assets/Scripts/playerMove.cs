using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerMove : MonoBehaviour
{
    [SerializeField] private float m_jumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float m_movementSmoothing = .05f;
    [SerializeField] private bool m_airControl = false;
    [SerializeField] private bool m_doubleJump = false;
    [SerializeField] private LayerMask m_whatIsGround;
    [SerializeField] private Transform m_groundCheck;

    const float k_groundedRadius = .2f;
    private bool m_isGrounded;
    private Rigidbody2D m_rigidBody2D;
    private bool m_isFacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_isGrounded;
        m_isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, k_groundedRadius, m_whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_isGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool jump)
    {
        if (m_isGrounded || m_airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_rigidBody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_rigidBody2D.velocity = Vector3.SmoothDamp(m_rigidBody2D.velocity, targetVelocity, ref m_Velocity, m_movementSmoothing);
            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_isFacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_isFacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_isGrounded && jump)
        {
            // Add a vertical force to the player.
            m_isGrounded = false;
            m_rigidBody2D.AddForce(new Vector2(0f, m_jumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_isFacingRight = !m_isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
