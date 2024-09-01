using System.Collections;
using UnityEngine;

public class RemoveScrew : MonoBehaviour
{
    private float timeHolding = 2f; // Time needed to hold before removing
    [SerializeField] private Animator screwAnimator;  // Reference to the Animator component

    private bool isHolding = false;
    private float elapsedTime = 0f;

    void Update()
    {
        // Detect mouse press
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isHolding = true;
                elapsedTime = 0f; // Reset elapsed time
                screwAnimator.SetBool("IsPressed", true); // Start the Screwing animation
                StartCoroutine(Action());
            }
        }

        // Detect mouse release
        if (Input.GetMouseButtonUp(0))
        {
            if (isHolding)
            {
                isHolding = false;
                screwAnimator.SetBool("IsPressed", false); // Stop the Screwing animation
            }
        }
    }

    IEnumerator Action()
    {
        // Wait until the screw is held for the required time or the user stops holding
        while (isHolding && elapsedTime < timeHolding)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (isHolding)
        {
            // Trigger the removal animation
            screwAnimator.SetTrigger("Removed");

            // Wait for the animation to finish before destroying the object
            // Use a fixed duration if the length is inconsistent
            float animationLength = screwAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);

            // Notify the GameLogic before destroying the object
            CaseMiniGameManager2.instance.RemoveScrew();
            Destroy(gameObject);
        }
        else
        {
            // Immediately reset the animation to Idle if not holding
            screwAnimator.SetBool("IsPressed", false);
            // Optionally reset the elapsed time here if needed
        }
    }
}
