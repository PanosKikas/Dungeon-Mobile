using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    public EquipableSO[] MainCharacterEquipment;

    private void Start()
    {
        MainCharacterEquipment = new EquipableSO[5];
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

    public void Equip(EquipableSO item)
    {
        int equippedIndex = (int)item.Type;
        EquipableSO oldItem = MainCharacterEquipment[equippedIndex];
        MainCharacterEquipment[equippedIndex] = item;
        
        if (oldItem != null)
        {
            Inventory.Instance.StoreToInventory(oldItem);
             
        }         
        
    }


    
}
