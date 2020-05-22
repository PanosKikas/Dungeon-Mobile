using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlotUI : MonoBehaviour
{

    Image armorIcon;

    private void OnEnable()
    {
        if (armorIcon == null)
        {
            armorIcon = transform.GetChild(0).GetComponent<Image>();
        }

        EquipableSO equipedSlot = CharacterEquipment.Instance.MainCharacterEquipment[transform.GetSiblingIndex()];
        if (equipedSlot != null)
        {
            armorIcon.enabled = true;
            armorIcon.sprite = equipedSlot.Icon;
        }
    }
}
