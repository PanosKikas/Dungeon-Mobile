using System;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class StatBarUI : MonoBehaviour
{
    private Slider slider;
    private TextMeshProUGUI uiText;

    private int currentValue;
    private int maxValue;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        uiText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Set(IObservable<float> curr, IObservable<float> max)
    {
        curr.Subscribe(x =>
        {
            currentValue = (int)x;
            UpdateBar();
        });
        max.Subscribe(x =>
        {
            maxValue = (int)x;
            UpdateBar();
        });
    }

    private void UpdateBar()
    {
        slider.value = (float)currentValue / maxValue;
        if (uiText != null)
        {
            uiText.text = $"{currentValue}/{maxValue}";
        }
    }
}