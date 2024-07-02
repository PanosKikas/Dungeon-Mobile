using DMT.Controllers;
using DMT.Pickups;
using UnityEngine;

namespace DMT.Interactables
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PickupObject : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 5f)] float attractionSpeed = 1f;

        private IPickable pickable;
        private Player target;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rb;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetPickup(IPickable pickup)
        {
            pickable = pickup;
            spriteRenderer.sprite = pickup.Icon;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player") && collision.gameObject.TryGetComponent<Player>(out var player))
            {
                if (pickable.TryPickUp(player))
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                target = collision.GetComponent<Player>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                target = null;
            }
        }

        private void FixedUpdate()
        {
            if (target is null)
            {
                return;
            }

            if (target.IsInventoryFull())
            {
                return;
            }

            var deltaMovement = Vector3
                .MoveTowards(transform.position, target.transform.position, attractionSpeed * Time.fixedDeltaTime);
            rb.MovePosition(deltaMovement);
        }
    }
}

