using System.Collections;
using System.Collections.Generic;
using DMT.Characters;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectSlotUI : MonoBehaviour
{
    [SerializeField] private Image characterIcon;
    
    public void SetTo(Character character)
    {
        characterIcon.sprite = character.Portrait;
    }
}
