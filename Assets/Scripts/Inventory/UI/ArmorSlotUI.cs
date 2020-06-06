using System.Collections;
using System.Collections.Generic;
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

        UpdateEquipmentSlotsUI();
    }

    void InitialiazeEquipmentSlots()
    {
        equipmentButtons = GetComponentsInChildren<Button>();
        armorIcons = new Image[equipmentButtons.Length];

        for (int i = 0; i < equipmentButtons.Length; i++)
        {
            armorIcons[i] = equipmentButtons[i].transform.GetChild(0).GetComponent<Image>();
        }
    }

    void UpdateEquipmentSlotsUI()
    {
        for (int i = 0; i < equipmentButtons.Length; ++i)
        {
            EquipableSO equipedSlot = CharacterEquipment.Instance.MainCharacterEquipment[i];

            if (equipedSlot != null)
            {
                armorIcons[i].enabled = true;
                armorIcons[i].sprite = equipedSlot.Icon;
            }
        }
    }
}
