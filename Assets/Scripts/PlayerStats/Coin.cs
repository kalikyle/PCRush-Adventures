using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    //private Rigidbody2D rb;
    //[SerializeField]

    //public float duration = 0.3f;


    //[SerializeField]

    //public int coinValue;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();

    //    // Disable gravity initially
    //    if (rb != null)
    //    {
    //        rb.gravityScale = 0;
    //    }


    //    //GetCoin(1);
    //}

    //internal void DestroyItem()
    //{
    //    GetComponent<CircleCollider2D>().enabled = false;
    //    StartCoroutine(AnimatePickUp());
    //}

    //private IEnumerator AnimatePickUp()
    //{
    //    Vector3 startScale = transform.localScale;
    //    Vector3 endScale = Vector3.zero;
    //    float currentTime = 0;
    //    while (currentTime < duration)
    //    {
    //        currentTime += Time.deltaTime;
    //        transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
    //        yield return null;
    //    }

    //    Destroy(gameObject);
    //}

    private Rigidbody2D rb;
    [SerializeField]
    private float duration = 0.3f;

    [SerializeField]
    public int coinValue;

    [SerializeField]
    private float magnetSpeed = 5f; // Speed at which the coin moves towards the player

    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Disable gravity initially
        if (rb != null)
        {
            rb.gravityScale = 0;
        }

        // Assuming the player has a tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Move the coin towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);
        }
    }

    //internal void ActivateMagnet()
    //{
    //    isMagnetActive = true;
    //}

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
