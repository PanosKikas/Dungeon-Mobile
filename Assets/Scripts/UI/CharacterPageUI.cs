using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT.UI.Screen
{
    public class CharacterPageUI : MonoBehaviour
    {
        [SerializeField]
        private EquipmentPanel equipmentPanel;

        [SerializeField]
        private CharacterPreviewUI characterPreview;

        public void SetTo(Character character)
        {
            equipmentPanel.SubscribeTo(character);
            characterPreview.SetTo(character);
        }
    }
}