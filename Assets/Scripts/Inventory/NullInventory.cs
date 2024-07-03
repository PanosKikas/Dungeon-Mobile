using System;
using System.Collections.Generic;
using DMT.Characters.Inventory;
using DMT.Pickups;
using UniRx;

namespace DMT.Characters.Inventory
{
    public class NullInventory : IInventory
    {
        public IReactiveCollection<ItemSlot> InventoryItems => new ReactiveCollection<ItemSlot>();

        public bool IsFull()
        {
            return false;
        }

        public void Store(IStorable item)
        {
            
        }

        public IEnumerable<ItemSlot> Slots => new List<ItemSlot>();
        public void RemoveItem(IStorable storable)
        {
            
        }
    }
}