using System;
using DMT.Controllers;
using UnityEngine;

namespace DMT.Pickups
{
    public abstract class Item : IPickable, IStorable, IEquatable<Item>
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract Sprite Icon { get; }

        public virtual bool TryPickUp(Player player)
        {
            if (player.Inventory.IsFull())
            {
                return false;
            }

            player.Inventory.Store(this);
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is Item other && this.Equals(other);
        }

        public bool Equals(Item other)
        {
            if (other == null)
            {
                return false;
            }

            return Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
