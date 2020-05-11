using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite openChestSprite;
    
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Interact()
    {

        base.Interact();
        OpenChest();
        
    }

    void OpenChest()
    {
        ChangeGraphics();
        this.enabled = false;
    }

    void ChangeGraphics()
    {
        spriteRenderer.sprite = openChestSprite;
    }

    
}
