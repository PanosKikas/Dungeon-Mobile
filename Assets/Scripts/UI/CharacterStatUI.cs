using System;
using System.Globalization;
using DMT.Characters.Stats;
using TMPro;
using UnityEngine;
using UniRx;
using Utils;

public class CharacterStatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayName;
    [SerializeField] private TextMeshProUGUI value;

    private IDisposable subscription;
    
    public void SubscribeTo(CharacterStat stat)
    {
        subscription?.Dispose();
        displayName.text = $"{stat.StatType.ToShortString()}: ";
        subscription = stat.Subscribe(StatChanged);
    }

    private void StatChanged(float newValue)
    {
        value.text = newValue.ToString(CultureInfo.InvariantCulture);
    }

    private void OnDestroy()
    {
        subscription?.Dispose();
        subscription = null;
    }
}
