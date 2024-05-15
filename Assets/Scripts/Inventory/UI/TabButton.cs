using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DMT.UI.Components
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerClickHandler
    {
        TabGroup tabGroup;

        [HideInInspector]
        public Image background;

        [SerializeField]
        private Sprite tabIdle;

        [SerializeField]
        private Sprite tabActive;

        [SerializeField]
        private GameObject page;

        private CanvasGroup pageCanvasGroup;

        private void Awake()
        {
            tabGroup = GetComponentInParent<TabGroup>();
            background = GetComponent<Image>();
            pageCanvasGroup = page?.GetComponent<CanvasGroup>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        public void Select()
        {
            background.sprite = tabActive;
            ShowPage();
        }

        private void ShowPage()
        {
            pageCanvasGroup.SetActive(true);
        }

        public void Deselect()
        {
            background.sprite = tabIdle;
            HidePage();
        }

        private void HidePage()
        {
            pageCanvasGroup.SetActive(false);
        }
    }
}