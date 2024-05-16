using DMT.UI.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class TabPageUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public UnityEvent OnShow;
    public UnityEvent OnHide;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        canvasGroup.SetActive(true);
        OnShow?.Invoke();
    }

    public void Hide()
    {
        canvasGroup.SetActive(false);
        OnHide?.Invoke();
    }
}
 