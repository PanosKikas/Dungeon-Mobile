using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;

public class StatsDisplayerUI : MonoBehaviour
{
    TextMeshProUGUI statsText;
    CharacterStats stats;

    private void Awake()
    {
        statsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
       // characterStats = StatsDatabase.Instance.GetMainCharacterStats();
    }

    private void Update()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("Physical Dmg: ").Append(stats.PhysicalDamage.Value).Append("\n");
        builder.Append("Crit % : ").Append(stats.CriticalChance.Value).Append("\n");
        builder.Append("Attk Speed: ").Append(stats.AutoAttackRate.Value).Append("\n");
        builder.Append("Divine Dmg: ").Append(stats.DivineDamage.Value).Append("\n");
        builder.Append("Divine Resist: ").Append(stats.DivinityResistance.Value).Append("\n");
        builder.Append("Stamina Regen: ").Append(stats.StaminaRegen.Value).Append("\n");
        builder.Append("Evasion % : ").Append(stats.EvasionChance.Value).Append("\n");
        builder.Append("Item Drop % : ").Append(stats.ItemDropRate.Value).Append("\n");
        statsText.text = builder.ToString();
    }
}
