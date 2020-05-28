using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HpUI : MonoBehaviour
{

    Slider HpSlider;

    TextMeshProUGUI textMeshPro;


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
    }


    private void Update()
    {
        float CurrentHealth = StatsDatabase.Instance.PlayerCharacterStats[0].CurrentHealth;
        float MaxHealth = StatsDatabase.Instance.PlayerCharacterStats[0].MaxHealth;
        HpSlider.value = CurrentHealth / MaxHealth;
        textMeshPro.text = string.Format("{0}/{1}", CurrentHealth, MaxHealth);
    }
}
