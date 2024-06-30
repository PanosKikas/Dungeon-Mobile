using System;
using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CharacterEquipmentPanel : MonoBehaviour
{
    private Character character;
    private EquipmentSlotUI[] equipmentSlotsUI;

    private readonly List<IDisposable> characterSubscriptions = new();
    
    private void Awake()
    {
        equipmentSlotsUI = GetComponentsInChildren<EquipmentSlotUI>();
    }

    public void SetTo(Character targetCharacter)
    {
        characterSubscriptions.DisposeAndClear();
        Assert.IsNotNull(targetCharacter, "Cannot subscribe equipment panel to null character.");
        character = targetCharacter;
        var equipment = targetCharacter.Equipment;
        for (var i = 0; i < equipment.EquipmentSlots.Length; ++i)
        {
            var equipmentSlot = equipment.EquipmentSlots[i];
            equipmentSlotsUI[i].SetTo(equipmentSlot);
            equipmentSlotsUI[i].OnSlotHeld.Subscribe(SlotHeld).AddTo(characterSubscriptions);
        }
    }

    private void SlotHeld(EquipmentSlotUI slotUI)
    {
        character.UnequipFrom(slotUI.equipmentSlot);
    }

    private void OnDestroy()
    {
        characterSubscriptions.DisposeAndClear();
    }
}
