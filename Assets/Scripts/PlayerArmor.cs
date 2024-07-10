using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerArmor : MonoBehaviour
{
    [SerializeField]
    public int currentArmor, maxArmor;

    [SerializeField]
    public bool isEmpty = false;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public void Start()
    {


    }

    public void Update()
    {
        if (currentArmor > maxArmor)
        {
            currentArmor = maxArmor;
        }
    }

    public void InitializeArmor(int healthValue)
    {
        currentArmor = healthValue;
        maxArmor = healthValue;
        isEmpty = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isEmpty)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentArmor -= amount;
        //healthSlider.value = currentHealth;

        if (currentArmor > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isEmpty = true;
            //Destroy(gameObject);
        }
    }
}
