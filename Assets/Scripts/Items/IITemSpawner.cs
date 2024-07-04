using UnityEngine;

namespace DMT.Pickups
{
    public interface IITemSpawner
    {
        public abstract void Spawn(ItemData itemData, Vector3 position);
    }
}