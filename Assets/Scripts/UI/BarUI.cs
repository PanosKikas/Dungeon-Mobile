using DMT.Character;
using DMT.Character.Stats;
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
    CharacterStats characterControllerStats;

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

        characterControllerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().stats;


    }
    
    private void Update()
    {
        float CurrentHealth = characterControllerStats.CurrentHealth;
        float MaxHealth = characterControllerStats.MaxHealth;
        HpSlider.value = CurrentHealth / MaxHealth;
        textMeshPro.text = string.Format("{0}/{1}", CurrentHealth, MaxHealth);
    }
}
