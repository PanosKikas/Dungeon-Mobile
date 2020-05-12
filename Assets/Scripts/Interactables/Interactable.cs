using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class Interactable : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{

    bool canInteract = false;
    [SerializeField]
    protected float interactableRadius = 3f;

    protected void Awake()
    {
        var collider = GetComponent<CircleCollider2D>();
        collider.radius = interactableRadius;
        collider.isTrigger = true;
    }


    public virtual void Interact()
    {
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        if (canInteract)
        {
            Interact();
        }
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
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

}
