using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Transform target = null;

    [SerializeField]
    float gravitationSpeed = 1f;

    private Item item;
    private PickupSO data;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetTo(PickupSO pickup)
    {
        this.data = pickup;
        spriteRenderer.sprite = pickup.Icon;
    }
    
    protected virtual void PickUp()
    {
        var controller = target.GetComponent<PlayerController>();
        var item = new Equipment(data);
        controller.Pickup(item);
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
