﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    public EquipmentData[] MainCharacterEquipment;

    private readonly int PotionIndex = (int)EquipmentType.Potion;

    private const int armorSlots = 6;

    private void Start()
    {
        MainCharacterEquipment = new EquipmentData[armorSlots];
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

    public void Equip(EquipmentData item)
    {
        int equippedIndex = (int)item.EquipmentType;
        

        if (IsPotion(item.EquipmentType))
        {
            equippedIndex = FindPotionEquipmentIndex();
        }

        EquipmentData oldItem = MainCharacterEquipment[equippedIndex];
        if (oldItem != null)
        {
            oldItem.Unequip();
          //  Inventory.Instance.TryStoreInventory(oldItem);

        }

        MainCharacterEquipment[equippedIndex] = item;
        
    }

    private int FindPotionEquipmentIndex()
    {
        return (MainCharacterEquipment[PotionIndex] == null) ?  PotionIndex : PotionIndex + 1;
    }

    bool IsPotion(EquipmentType type)
    {
        return type == EquipmentType.Potion;
    }


    
}
