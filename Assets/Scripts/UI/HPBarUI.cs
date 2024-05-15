using DMT.Characters;
using DMT.Characters.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    Slider slider;

    TextMeshProUGUI hpText;
    CharacterStats stats;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        hpText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetToStats(CharacterStats stats)
    {
        this.stats = stats;
    }

    private void Update()
    {
        if (stats == null)
        {
            return;
        }

        float CurrentHealth = stats.CurrentHealth;
        float MaxHealth = stats.MaxHealth;
        slider.value = CurrentHealth / MaxHealth;
        hpText.text = string.Format("{0}/{1}", CurrentHealth, MaxHealth);
    }
}
