using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DMT.UI.Screen
{
    public class CharacterPageUI : MonoBehaviour
    {
        [SerializeField]
        private EquipmentPanel equipmentPanel;

        [SerializeField]
        private CharacterPreviewUI characterPreview;

        [SerializeField]
        private Image tabImage;

        [SerializeField]
        private CanvasGroup canvasGroup;

        public void SetTo(Character character)
        {
            equipmentPanel.SubscribeTo(character);
            characterPreview.SetTo(character);
            tabImage.transform.parent.GetComponent<CanvasGroup>().SetActive(true);
            tabImage.sprite = character.Portrait;
        }

        public void Disable()
        {
            tabImage.transform.parent.GetComponent<CanvasGroup>().SetActive(false);
        }

    }
}