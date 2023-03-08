using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentSO : PickupSO
{
    [FormerlySerializedAs("Type")] public EquipmentSlotType slotType;
}
