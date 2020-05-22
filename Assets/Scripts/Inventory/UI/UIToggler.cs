using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggler : MonoBehaviour
{
    [SerializeField]
    GameObject inventory;

    [SerializeField]
    GameObject characterStats;

    private void Start()
    {
        ToggleInventory();
    }

    public void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }

    public void ToggleCharacterStats()
    {
        characterStats.SetActive(!characterStats.activeSelf);
    }
    
}
