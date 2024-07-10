using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class BattleTransitionAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private CanvasGroup canvasGroup;
    
    public Subject<Unit> OnFinished = new();

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        animator.enabled = true;
    }

    public void TransitionFinished()
    {
        OnFinished?.OnNext(new());
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        animator.enabled = false;
    }

    private void OnDisable()
    {
        animator.enabled = false;
    }
}
