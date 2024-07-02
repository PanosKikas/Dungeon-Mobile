using DMT.Characters.Inventory;
using DMT.Pickups;
using UniRx;
using UnityEngine.Assertions;

namespace DMT.Characters.Equipment
{
    public class EquipmentSlot
    {
        private readonly Character character;
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
            return CurrentEquippedItem.Value == null;
        }
    }

    public class CharacterEquipment
    {
        private const int TotalSlots = 5;
        public EquipmentSlot[] EquipmentSlots { get; } = new EquipmentSlot[TotalSlots];
        private readonly IInventory inventory;

        public CharacterEquipment(Character character, IInventory inventory = null)
        {
            this.inventory = inventory;
            for (var i = 0; i < EquipmentSlots.Length; ++i)
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
            Assert.IsNotNull(itemOnSlot, "Cannot unequip slot item because it is empty.");
            slot.Unequip();
            inventory?.Store(itemOnSlot);
        }
    }
}
