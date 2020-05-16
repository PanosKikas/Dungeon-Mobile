using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Pickup<T> : MonoBehaviour
{

   
    public PlayerCharacterStats playerStats;

    public T PickupStats;


    protected virtual void Start()
    {

        
        playerStats = StatsDatabase.Instance.PlayerCharacterStats[0];
        
    }

    protected virtual void PickUp() 
    {
        Destroy(gameObject);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {   
            
            PickUp();     
        }
    }

    
}

