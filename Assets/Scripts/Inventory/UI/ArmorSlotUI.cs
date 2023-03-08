using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlotUI : MonoBehaviour
{
    Button[] equipmentButtons;
    Image[] armorIcons;

    private void OnEnable()
    {
        if (equipmentButtons == null)
        {
            InitialiazeEquipmentSlots();         
        }

        UpdateEquipmentSlotsUI(null);
    }

    private void InitialiazeEquipmentSlots()
    {
        equipmentButtons = GetComponentsInChildren<Button>();
        armorIcons = new Image[equipmentButtons.Length];

        for (int i = 0; i < equipmentButtons.Length; i++)
        {
            armorIcons[i] = equipmentButtons[i].transform.GetChild(0).GetComponent<Image>();
        }
    }

    void UpdateEquipmentSlotsUI(CharacterEquipment characterEquipment)
    {
        if (characterEquipment == null)
        {
            return;
        }
        
        int i = 0;
        var equipmentSlots = Enum.GetValues(typeof(EquipmentSlotType)).Cast<EquipmentSlotType>();
        foreach(var slot in equipmentSlots)
        {
            var equippable = characterEquipment.Equipment[slot];

            if (equippable != null)
            {
                armorIcons[i].enabled = true;
                armorIcons[i].sprite = ((Item)equippable).Icon;
            }
            ++i; 
        }
    }
}
