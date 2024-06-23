using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]

    public float duration = 0.3f;


    [SerializeField]

    public int HeartValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Disable gravity initially
        if (rb != null)
        {
            rb.gravityScale = 0;
        }


        //GetCoin(1);
    }

    internal void DestroyItem()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(AnimatePickUp());
    }

    private IEnumerator AnimatePickUp()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}
