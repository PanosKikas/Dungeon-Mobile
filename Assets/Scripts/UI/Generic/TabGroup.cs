using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace DMT.UI.Components
{
    public class TabGroup : MonoBehaviour
    {
        private TabButtonUI currentSelectedTab;

        [SerializeField]
        private TabButtonUI initialSelectedTab;

        private void Start()
        {
            initialSelectedTab.Select();
            currentSelectedTab = initialSelectedTab;
        }

        public void OnTabSelected(TabButtonUI tabButton)
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
