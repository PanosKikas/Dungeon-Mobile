using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    HP,
    Mana,
    Endurance,
    Experience
}

public class BarUI : MonoBehaviour
{
    public BarType Type;
    Slider HpSlider;

    TextMeshProUGUI textMeshPro;
    CharacterStats characterStats;

    int targetCurrentStat; 
    CharacterStat targetMaxStat;

    private void OnEnable()
    {
        if (HpSlider == null)
        {
            HpSlider = GetComponent<Slider>();
        }

        if (textMeshPro == null)
        {
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        }

        characterStats = StatsDatabase.Instance.GetMainCharacterStats();

         
    }
    
    private void Update()
    {
        float CurrentHealth = characterStats.Health;
        float MaxHealth = characterStats.Data.MaxHealth;
        HpSlider.value = CurrentHealth / MaxHealth;
        textMeshPro.text = string.Format("{0}/{1}", CurrentHealth, MaxHealth);
    }
}
