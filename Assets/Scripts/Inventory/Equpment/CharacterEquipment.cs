using DMT.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot
{
    public event Action<IEquipable> OnItemChanged;
    public IEquipable CurrentEquipped { get; private set; }

    public void Equip(IEquipable equipable)
    {
        CurrentEquipped = equipable;
        OnItemChanged?.Invoke(equipable);
    }
    
    public bool IsEmpty()
    {
        return CurrentEquipped == null;
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
            EquipmentSlots[i] = new EquipmentSlot();
        }
    }

    public void Equip(IEquipable equipment)
    {
        int equippedIndex = (int)equipment.EquipmentType;
        var slot = EquipmentSlots[equippedIndex];
        if (!slot.IsEmpty())
        {
            var oldItem = slot.CurrentEquipped;
            oldItem.UnequipFrom(character);
            inventory?.TryStore(oldItem as IStorable);
        }

        slot.Equip(equipment);
    }
}
