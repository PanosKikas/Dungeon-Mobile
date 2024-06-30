using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.Assertions;

public class AnnouncementPopup : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private Player player;

    [SerializeField] private float fadeInDuration = 0.3f;

    [SerializeField] private float fadeOutDuration = 0.3f;

    [SerializeField] private float waitDuration = 1.5f;

    [SerializeField] private TextMeshProUGUI shownText;

    private readonly List<IDisposable> subscriptions = new();
    private Sequence sequence;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Assert.IsNotNull(canvasGroup, "No canvas groups in character added to party popup.");
        Assert.IsNotNull(player, "Player was null in character added to party popup.");
    }

    private void Start()
    {
        player.characterParty.ObserveAdd.Subscribe(c =>
            Show($"{c.Value.CharacterName} has joined the party")).AddTo(subscriptions);
        player.characterParty.ObserveRemove.Subscribe(c =>
            Show($"{c.Value.CharacterName} has left the party")).AddTo(subscriptions);
    }

    private void Show(string text)
    {
        shownText.text = text;
        sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1f, fadeInDuration).SetEase(Ease.InCubic));
        sequence.AppendInterval(waitDuration);
        sequence.Append(canvasGroup.DOFade(0f, fadeOutDuration).SetEase(Ease.OutCubic));
        sequence.Play();
    }

    private void OnDestroy()
    {
        subscriptions.DisposeAndClear();
    }
}