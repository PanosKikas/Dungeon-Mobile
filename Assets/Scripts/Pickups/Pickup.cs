using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public abstract class Pickup : MonoBehaviour
{

   

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
