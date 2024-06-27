using System;
using DMT.Characters;
using DMT.Characters.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class StatBarUI : MonoBehaviour
{
    private Slider slider;
    private TextMeshProUGUI hpText;

    private int currentValue;
    private int maxValue;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        hpText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SubscribeTo(IObservable<int> curr, IObservable<float> max)
    {
        curr.Subscribe(x =>
        {
            currentValue = x;
            UpdateHealthBar();
        });
        max.Subscribe(x =>
        {
            maxValue = (int)x;
            UpdateHealthBar();
        });
    }

    private void UpdateHealthBar()
    {
        slider.value = (float)currentValue / maxValue;
        hpText.text = $"{currentValue}/{maxValue}";
    }
}
