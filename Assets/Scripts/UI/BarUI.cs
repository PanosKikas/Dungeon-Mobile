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
    CharacterController characterControllerStats;

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

        characterControllerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterController>();

         
    }
    
    private void Update()
    {
        float CurrentHealth = characterControllerStats.Health;
        float MaxHealth = characterControllerStats.InitialStats.MaxHealth;
        HpSlider.value = CurrentHealth / MaxHealth;
        textMeshPro.text = string.Format("{0}/{1}", CurrentHealth, MaxHealth);
    }
}
