using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DMT.UI.Components;
using UnityEngine.UI;

namespace DMT.UI.Screen
{
    public class CharacterPageUI : TabPageUI
    {
        [SerializeField]
        private EquipmentPanel equipmentPanel;

        [SerializeField]
        private CharacterPreviewUI characterPreview;

        public void SetTo(Character character)
        {
            equipmentPanel.SubscribeTo(character);
            characterPreview.SetTo(character);
            tabButton.SetIcon(character.Portrait);
            Enable();
        }
    }
}