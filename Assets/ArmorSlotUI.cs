using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlotUI : MonoBehaviour
{

    Image armorIcon;
    private void Awake()
    {
        armorIcon = transform.GetChild(0).GetComponent<Image>();
    }
    

    private void Start()
    {
        armorIcon.enabled = false;
    }

    public void UpdateArmorSlot()
    {
        EquipableSO equipedSlot = CharacterEquipment.Instance.MainCharacterEquipment[transform.GetSiblingIndex()];
        if (equipedSlot != null)
        {
            armorIcon.enabled = true;
            armorIcon.sprite = equipedSlot.Icon;
        }
    }
}
