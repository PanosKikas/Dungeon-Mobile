using System;
using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DMT.UI.Components;
using UniRx;
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

        public Subject<Character> OnShow = new();
        public Subject<Character> OnHide = new();

        public void SubscribeTo(Character character)
        {
            this.character = character;
            equipmentPanel.SubscribeTo(character);
            characterPreview.SetTo(character);
        }

        public void Show()
        {
            if (character == null)
            {
                throw new ArgumentException("No character for this page.");
            }
            
            characterPreviewAnimator.ShowFor(character);
            OnShow.OnNext(character);
        }

        public void Hide()
        {
            characterPreviewAnimator.Hide();
            OnHide.OnNext(character);
        }
    }
}