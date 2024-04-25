using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggler : MonoBehaviour
{
    [SerializeField]
    CanvasGroup inventory;

    [SerializeField]
    GameObject characterStats;

    public void ToggleInventory()
    {
        inventory.alpha = inventory.alpha == 0 ? 1 : 0;
    }

    public void ToggleCharacterStats()
    {
        characterStats.SetActive(!characterStats.activeSelf);
    }
    
}
