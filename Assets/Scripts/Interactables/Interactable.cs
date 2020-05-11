using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Interactable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    float interactableRadius = 3f;

    bool canInteract = false;

    private void Awake()
    {
        var collider = GetComponent<CircleCollider2D>();
        collider.radius = interactableRadius;
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("interact");
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        if (canInteract)
        {
            Interact();
        }
    }
}
