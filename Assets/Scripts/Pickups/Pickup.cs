using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Pickup : MonoBehaviour
{

    protected virtual void PickUp() 
    {
        Debug.Log("Picked up"); 
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
