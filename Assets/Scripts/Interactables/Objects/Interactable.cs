﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DMT.Interactables
{
    public abstract class Interactable : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
        [HideInInspector] public bool canInteract;
        [SerializeField] public float interactableRadius = 3f;
        
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
    }
}
