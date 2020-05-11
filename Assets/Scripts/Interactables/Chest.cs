using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Chest : Interactable
{

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite openChestSprite;

    [SerializeField]
    Light2D light;

    private void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {

        base.Interact();
        OpenChest();
        
    }

    void OpenChest()
    {
        ChangeGraphics();
        OpenLight();
        this.enabled = false;
    }

    void ChangeGraphics()
    {
        spriteRenderer.sprite = openChestSprite;
    }

    void OpenLight()
    {
        light.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactableRadius);
    }

}
