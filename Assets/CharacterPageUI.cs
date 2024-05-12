using DMT.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPageUI : MonoBehaviour
{
    [SerializeField]
    private ArmorSlotUI armorSlotUI;

    [SerializeField]
    private CharacterPreviewUI characterPreview;

    public void SetTo(Character character)
    {
        armorSlotUI.SubscribeTo(character.Equipment);
        characterPreview.SetTo(character);
    }
}
