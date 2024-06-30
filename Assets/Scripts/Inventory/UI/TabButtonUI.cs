using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DMT.UI.Components
{
    [RequireComponent(typeof(Image))]
    public class TabButtonUI : MonoBehaviour, IPointerClickHandler
    {
        TabGroup tabGroup;

        [HideInInspector]
        public Image background;

        [SerializeField]
        private Sprite tabIdle;

        [SerializeField]
        private Sprite tabActive;

        [SerializeField]
        private Image tabIcon;

        private CanvasGroup canvasGroup;

        [SerializeField]
        private TabPageUI tabPage;

        private void Awake()
        {
            tabGroup = GetComponentInParent<TabGroup>();
            background = GetComponent<Image>();
            canvasGroup = GetComponent<CanvasGroup>();
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
            tabPage.Show();
        }

        public void Deselect()
        {
            background.sprite = tabIdle;
            HidePage();
        }

        private void HidePage()
        {
            tabPage.Hide();
        }

        public void Enable()
        {
            canvasGroup.SetActive(true);
        }

        public void Disable()
        {
            Deselect();
            canvasGroup.SetActive(false);
            tabPage.Hide();
        }

        public void SetIcon(Sprite icon)
        {
            tabIcon.sprite = icon;
        }
    }
}