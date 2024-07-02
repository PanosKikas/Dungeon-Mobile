using DMT.Characters;

namespace DMT.Pickups
{
    public interface IEquipable : IStorable
    {
        void EquipOn(Character character);
        void UnequipFrom(Character character);
        EquipmentType EquipmentType { get; }
    }
}