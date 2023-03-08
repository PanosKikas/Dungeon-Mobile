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
    Character character;

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

        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }
    
    private void Update()
    {
        float currentHealth = character.Health;
        float maxHealth = character.Stats.MaxHealth.Value;
        HpSlider.value = currentHealth / maxHealth;
        textMeshPro.text = string.Format("{0}/{1}", currentHealth, maxHealth);
    }
}
