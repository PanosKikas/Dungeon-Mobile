using DMT.UI.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DMT.UI.Screen
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        [SerializeField]
        private InventoryPanel inventoryUI;

        private CanvasGroup canvasGroup;

        [SerializeField]
        private CharacterPanel[] characterPages;

        [SerializeField]
        private TabButtonUI[] characterTabs;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
            var charactersInParty = player.Characters.ToArray();
            for (int i = 0; i < characterPages.Length; ++i)
            {
                if (i >= charactersInParty.Length)
                {
                    characterTabs[i].Disable();
                    continue;
                }
                characterTabs[i].SetIcon(charactersInParty[i].Portrait);
                characterPages[i].SetTo(charactersInParty[i]);
            }

            inventoryUI.Initialize(player.Inventory, charactersInParty);
        }

        public void Show()
        {
            canvasGroup.SetActive(true);
        }

        public void Hide()
        {
            canvasGroup.SetActive(false);
        }
    }
}
