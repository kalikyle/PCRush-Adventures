using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int currentHealth, maxHealth;

  
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    public bool isDead = false;

    public void Start()
    {

        
    }

    public void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;
        //healthSlider.value = currentHealth;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            //Destroy(gameObject);
        }
    }
}
