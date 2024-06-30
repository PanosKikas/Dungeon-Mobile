using System;
using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DMT.UI.Components;
using UniRx;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DMT.UI.Screen
{
    public class CharacterPanel : MonoBehaviour
    {
        [FormerlySerializedAs("equipmentPanel")] [SerializeField]
        private CharacterEquipmentPanel characterEquipmentPanel;

        [SerializeField]
        private CharacterPreviewUI characterPreview;

        [SerializeField]
        private CharacterPreviewAnimator characterPreviewAnimator;

        private Character character;

        public Subject<Character> OnShow = new();
        public Subject<Character> OnHide = new();

        public void Set(Character character)
        {
            this.character = character;
            characterEquipmentPanel.SetTo(character);
            characterPreview.SetTo(character);
        }

        public void Show()
        {
            Assert.IsNotNull(character, "Showing character UI for null character.");
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