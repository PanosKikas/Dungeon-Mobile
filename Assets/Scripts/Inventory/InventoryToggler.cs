using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggler : MonoBehaviour
{
    [SerializeField]
    GameObject inventory;

    public void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}
