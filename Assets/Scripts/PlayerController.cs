using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    //private bool isMoving;
    private Vector2 input;

    public LayerMask solidObjectsLayer;

    public Rigidbody2D r2d;


    private Animator animator;
    public PlayerTeleport playerTeleport;


    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

  private void Update()
    {
        //if (!playerTeleport.DeskPanel.activeSelf)
        //{
        //    if (!isMoving)
        //    {
        //        input.x = Input.GetAxisRaw("Horizontal");
        //        input.y = Input.GetAxisRaw("Vertical");

        //        if (input != Vector2.zero)
        //        {
        //            // Normalize the input vector to allow diagonal movement
        //            input.Normalize();

        //            animator.SetFloat("moveX", input.x);
        //            animator.SetFloat("moveY", input.y);

        //            // Calculate velocity vector based on input and speed
        //            Vector2 velocity = input * moveSpeed;

        //            // Apply velocity to Rigidbody2D
        //            r2d.velocity = velocity;

        //            // Set isMoving flag
        //            Move(input);
        //        }
        //        else
        //        {
        //            // Stop moving animation
        //            animator.SetBool("isMoving", false);

        //            // Reset velocity to stop movement
        //            r2d.velocity = Vector2.zero;

        //            // Set isMoving flag
        //            //isMoving = false;
        //        }
        //    }
        //    else
        //    {
        //        // If the desk is open, stop the player's movement
        //        r2d.velocity = Vector2.zero;
        //        animator.SetBool("isMoving", false);
        //        isMoving = false;
        //    }
        //}

        if (!playerTeleport.DeskPanel.activeSelf)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Normalize the input vector to allow diagonal movement
            input.Normalize();

            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);

            // Calculate velocity vector based on input and speed
            Vector2 velocity = input * moveSpeed;

            // Apply velocity to Rigidbody2D
            r2d.velocity = velocity;

            // Set isMoving flag based on input magnitude
            animator.SetBool("isMoving", input.magnitude > 0);

            // Adjust player's rotation based on movement direction
            if (input != Vector2.zero)
            {
                float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the collision is with an object on the solidObjectsLayer
        if (((1 << collision.gameObject.layer) & solidObjectsLayer) != 0)
        {
            // Get the contact point with the collider
            ContactPoint2D contact = collision.contacts[0];

            // Calculate the angle between the contact normal and player's velocity
            float angle = Vector2.Angle(contact.normal, r2d.velocity);

            // If the angle is close to 90 degrees, adjust the velocity to slide
            if (Mathf.Abs(angle - 90f) < 45f)
            {
                // Calculate the slide direction using the contact normal
                Vector2 slideDirection = Vector2.Reflect(r2d.velocity.normalized, contact.normal);

                // Apply the slide direction as velocity
                r2d.velocity = slideDirection * moveSpeed;
            }
        }
    }
    private void Move(Vector2 movement)
    {
        // Calculate velocity vector based on input and speed
        Vector2 velocity = movement * moveSpeed;

        // Apply velocity to Rigidbody2D
        r2d.velocity = velocity;

        // Start moving animation
        animator.SetBool("isMoving", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object on the solidObjectsLayer
        if (((1 << collision.gameObject.layer) & solidObjectsLayer) != 0)
        {
            // Stop movement when colliding with an obstacle
            //isMoving = false;
        }
    }


}
