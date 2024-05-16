using DMT.UI.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class TabPageUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField]
    protected TabButtonUI tabButton;

    public virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        canvasGroup.SetActive(true);
    }

    public virtual void Hide()
    {
        canvasGroup.SetActive(false);
    }

    public virtual void Enable()
    {
        tabButton.Enable(this);
    }

    public virtual void Disable()
    {
        tabButton.Disable();
        Hide();
    }
}
 