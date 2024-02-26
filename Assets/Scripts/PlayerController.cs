using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    public LayerMask solidObjectsLayer;


    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }



    //private void Update()
    //{
    //    if (!isMoving)
    //    {
    //        input.x = Input.GetAxisRaw("Horizontal");
    //        input.y = Input.GetAxisRaw("Vertical");

    //        //remove the diagonal movement
    //        if (input.x != 0) input.y = 0;


    //        if (input != Vector2.zero)
    //        {
    //            animator.SetFloat("moveX", input.x);
    //            animator.SetFloat("moveY", input.y);

    //            var targetPos = transform.position;
    //            targetPos.x += input.x;
    //            targetPos.y += input.y;

    //            if (isWalkable(targetPos))
    //            {
    //                StartCoroutine(Move(targetPos));
    //            }


    //        }

    //    }
    //    animator.SetBool("isMoving", isMoving);
    //}
    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                // Normalize the input vector to allow diagonal movement
                input.Normalize();

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position + new Vector3(input.x, input.y, 0);

                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move (Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }


    private bool isWalkable(Vector3 targetPos)
    {
       if( Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer)!= null)
        {
            return false;
        }

       return true;
    }
}
