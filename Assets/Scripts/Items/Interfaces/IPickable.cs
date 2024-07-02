using DMT.Controllers;
using UnityEngine;

namespace DMT.Pickups
{
    public interface IPickable
    {
        public Sprite Icon { get; }
        bool TryPickUp(Player player);
    }
}