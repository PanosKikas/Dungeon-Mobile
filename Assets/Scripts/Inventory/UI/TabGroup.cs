using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DMT.UI.Components
{
    public class TabGroup : MonoBehaviour
    {
        private TabButton currentSelectedTab;

        [SerializeField]
        private TabButton initialSelectedTab;

        private void Start()
        {
            initialSelectedTab.Select();
            currentSelectedTab = initialSelectedTab;
        }

        public void OnTabSelected(TabButton tabButton)
        {
            if (currentSelectedTab == tabButton)
            {
                return;
            }

            currentSelectedTab?.Deselect();
            currentSelectedTab = tabButton;
            currentSelectedTab.Select();
        }
    }
}
