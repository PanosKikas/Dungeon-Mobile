using System.Collections.Generic;
using DMT.Pickups;
using UniRx;

namespace DMT.Characters.Inventory
{
    public interface IInventory
    {
        public IReactiveCollection<ItemSlot> InventoryItems { get; }
        bool IsFull();
        void Store(IStorable item);
        IEnumerable<ItemSlot> Slots { get; }
        void RemoveItem(IStorable storable);
        bool ContainsItem(IStorable storable);
    }
}