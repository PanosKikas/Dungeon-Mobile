using DMT.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EquipmentSlot
{
    private Character character;
    public ReactiveProperty<IEquipable> CurrentEquippedItem { get; } = new();

    public EquipmentSlot(Character character)
    {
        this.character = character;
    }

    public void Equip(IEquipable equipable)
    {
        CurrentEquippedItem.Value = equipable;
        equipable.EquipOn(character);
    }

    public void Unequip()
    {
        if (CurrentEquippedItem.Value == null)
        {
            return;
        }

        CurrentEquippedItem.Value.UnequipFrom(character);
        CurrentEquippedItem.Value = null;
    }

    public bool IsEmpty()
    {
        return CurrentEquippedItem == null;
    }
}

public class CharacterEquipment
{
    public EquipmentSlot[] EquipmentSlots { get; } = new EquipmentSlot[totalSlots];
    private IInventory inventory;
    private const int totalSlots = 5;
    private Character character;

    public CharacterEquipment(Character character, IInventory inventory = null)
    {
        this.character = character;
        this.inventory = inventory;
        for (int i = 0; i < EquipmentSlots.Length; ++i)
        {
            EquipmentSlots[i] = new EquipmentSlot(character);
        }
    }

    public void Equip(IEquipable equipment)
    {
        var equippedIndex = (int)equipment.EquipmentType;
        var slot = EquipmentSlots[equippedIndex];
        IEquipable oldItem = null;
        if (!slot.IsEmpty())
        {
            oldItem = slot.CurrentEquippedItem.Value;
        }
        
        slot.Equip(equipment);
        if (oldItem != null)
        {
            inventory?.Store(oldItem);
        }
    }

    public void Unequip(EquipmentSlot slot)
    {
        var itemOnSlot = slot.CurrentEquippedItem.Value;
        slot.Unequip();
        inventory?.Store(itemOnSlot);
    }
}
