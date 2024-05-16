using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DMT.UI.Components;
using UnityEngine.UI;

namespace DMT.UI.Screen
{
    public class CharacterPanel : MonoBehaviour
    {
        [SerializeField]
        private EquipmentPanel equipmentPanel;

        [SerializeField]
        private CharacterPreviewUI characterPreview;

        [SerializeField]
        private CharacterPreviewAnimator characterPreviewAnimator;

        private Character character;

        public void SetTo(Character character)
        {
            this.character = character;
            equipmentPanel.SubscribeTo(character);
            characterPreview.SetTo(character);
        }

        public void OnShow()
        {
            characterPreviewAnimator.ShowFor(character);
        }

        public void OnHide()
        {
            characterPreviewAnimator.Hide();
        }
    }
}