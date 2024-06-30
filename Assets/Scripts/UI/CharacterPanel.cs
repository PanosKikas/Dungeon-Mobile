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

        [SerializeField] private CharacterPreviewUI characterPreview;

        [SerializeField] private CharacterPreviewAnimator characterPreviewAnimator;

        private Character character;

        public Subject<Character> OnShow = new();
        public Subject<Character> OnHide = new();

        private bool isShowing;

        private Player playerController;

        public void Set(Player player, Character character)
        {
            playerController = player;
            this.character = character;
            Assert.IsNotNull(this.character, "Character passed was null");
            characterEquipmentPanel.SetTo(character);
            characterPreview.SetTo(character);
        }

        public void Clear()
        {
            Hide();
        }

        public void Show()
        {
            Assert.IsNotNull(character, "Showing character UI for null character.");
            if (isShowing)
            {
                return;
            }
            isShowing = true;
            characterPreviewAnimator.ShowFor(character);
            OnShow.OnNext(character);
        }

        public void Hide()
        {
            if (!isShowing)
            {
                return;
            }

            isShowing = false;
            characterPreviewAnimator.Hide();
            OnHide.OnNext(character);
        }

        public void AbandonCharacter()
        {
            if (playerController.characterParty.Count == 1)
            {
                return;
            }
            
            playerController.RemoveFromParty(character);
        }
    }
}