using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabGroup : MonoBehaviour
{
    public TabButton[] tabButtons;

    public GameObject[] pageAreas;

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    TabButton selectedTab;

    [SerializeField]
    TabButton initialSelectedButton;

    private void Awake()
    {
        tabButtons = GetComponentsInChildren<TabButton>();
    }

    private void Start()
    {
        ResetPages();
        OnTabSelected(initialSelectedButton);
    }

    public void OnTabEnter(TabButton tabButton)
    {
        if (selectedTab == null || selectedTab != tabButton)
        {
            ResetTabs();
            tabButton.background.sprite = tabHover;
        }     
    }

    public void OnTabExit(TabButton tabButton)
    {
        ResetTabs();
        
    }

    public void OnTabSelected(TabButton tabButton)
    {
        ResetTabs();

        if (selectedTab != null)
        {
            int oldSelectedIndex = selectedTab.transform.GetSiblingIndex();
            pageAreas[oldSelectedIndex].SetActive(false);
        }

        selectedTab = tabButton;
        tabButton.background.sprite = tabActive;
        int index = tabButton.transform.GetSiblingIndex();
        pageAreas[index].SetActive(true);
        
    }

    void ResetPages()
    {
        foreach (var page in pageAreas)
        {
            page.SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach (var tabButton in tabButtons)
        {
            if (selectedTab != null && tabButton == selectedTab)
                continue;
            tabButton.background.sprite = tabIdle;
        }
    }
}
