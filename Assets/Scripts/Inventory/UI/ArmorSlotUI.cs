using DMT.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlotUI : MonoBehaviour
{
    [SerializeField]
    private Character character;
    EquipmentSlotUI[] equipmentSlotsUI;

    private void Awake()
    {
        equipmentSlotsUI = GetComponentsInChildren<EquipmentSlotUI>();
    }

    public void SubscribeTo(CharacterEquipment equipment)
    {
        for (int i = 0; i < equipment.EquipmentSlots.Length; ++i)
        {
            var slot = equipment.EquipmentSlots[i];
            equipmentSlotsUI[i].SubscribeTo(slot);
        }
    }
}
