using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT.Interactables
{
    public class RadiusHandler : MonoBehaviour
    {
        Interactable interactable;

        private void Awake()
        {
            interactable = GetComponentInParent<Interactable>();
            var collider = GetComponent<CircleCollider2D>();
            collider.radius = interactable.interactableRadius;
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                interactable.canInteract = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                interactable.canInteract = false;
            }
        }
    }
}