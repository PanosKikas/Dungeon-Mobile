using DMT.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot
{
    private Character character;

    public event Action<IEquipable> OnItemChanged;
    public IEquipable CurrentEquippedItem { get; private set; }

    public EquipmentSlot(Character character)
    {
        this.character = character;
    }

    public void Equip(IEquipable equipable)
    {
        CurrentEquippedItem = equipable;
        equipable.EquipOn(character);
        OnItemChanged?.Invoke(equipable);
    }

    public void Unequip()
    {
        if (CurrentEquippedItem == null)
        {
            return;
        }

        CurrentEquippedItem.UnequipFrom(character);
        CurrentEquippedItem = null;
        OnItemChanged?.Invoke(null);
    }

    public bool IsEmpty()
    {
        return CurrentEquippedItem == null;
    }
}

public class CharacterEquipment
{
    public EquipmentSlot[] EquipmentSlots { get; private set; } = new EquipmentSlot[totalSlots];
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
        int equippedIndex = (int)equipment.EquipmentType;
        var slot = EquipmentSlots[equippedIndex];
        IEquipable oldItem = null;
        if (!slot.IsEmpty())
        {
            oldItem = slot.CurrentEquippedItem;
        }

        slot.Unequip();
        slot.Equip(equipment);
        if (oldItem != null)
        {
            inventory?.TryStore(oldItem);
        }
    }

    public void Unequip(EquipmentSlot slot)
    {
        var itemOnSlot = slot.CurrentEquippedItem;
        slot.Unequip();
        inventory?.TryStore(itemOnSlot);
    }
}
