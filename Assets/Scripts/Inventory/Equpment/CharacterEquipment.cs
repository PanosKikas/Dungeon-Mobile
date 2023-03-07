using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    public IEquipable[] MainCharacterEquipment;

    private const int armorSlots = 6;

    private void Start()
    {
       // MainCharacterEquipment = new EquipableSO[armorSlots];
    }

    #region Singletton
    public static CharacterEquipment Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion

    public void Equip(IEquipable item)
    {
        int equippedIndex = (int)item.Type;

        IEquipable oldItem = MainCharacterEquipment[equippedIndex];
        if (oldItem != null)
        {
            oldItem.Unequip();
            Inventory.Instance.TryStoreInventory(oldItem as Item);
        }

        MainCharacterEquipment[equippedIndex] = item;
    }
}
