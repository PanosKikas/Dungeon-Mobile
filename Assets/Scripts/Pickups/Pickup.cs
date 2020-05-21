using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Pickup : MonoBehaviour
{
    
    public PlayerCharacterStats playerStats;

    public PickupSO PickupStats;

    Transform target = null;
    [SerializeField]
    float gravitationSpeed = 1f;

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

    private void Update()
    {
        if (target)
        {
            transform.position = Vector3
                .MoveTowards(transform.position, target.position, gravitationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target != null && collision.CompareTag("Player"))
            target = null;
    }

}

