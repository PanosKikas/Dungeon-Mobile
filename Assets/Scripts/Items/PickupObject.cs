using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PickupObject : MonoBehaviour
{
    [SerializeField]
    float gravitationSpeed = 1f;

    private ICollectable item;
    private Transform currentTarget = null;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeTo(ICollectable collectable)
    {
        item = collectable;
        spriteRenderer.sprite = collectable.Icon;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && collision.gameObject.TryGetComponent<Player>(out var player))
        {
            if (player.Pickup(item))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentTarget = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentTarget = null;
        }
    }

    private void FixedUpdate()
    {
        if (currentTarget is null)
        {
            return;
        }

        if (currentTarget.GetComponent<Player>().IsInventoryFull())
        {
            return;
        }

        var deltaMovement = Vector3
                .MoveTowards(transform.position, currentTarget.position, gravitationSpeed * Time.fixedDeltaTime);
        rb.MovePosition(deltaMovement);
    }
}

