using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using DMT.Character.Stats;

public class StatsDisplayerUI : MonoBehaviour
{
    TextMeshProUGUI statsText;
    CharacterStats characterStats;

    private void Awake()
    {
        statsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //characterStats = StatsDatabase.Instance.GetMainCharacterStats();
    }

    private void Update()
    {
       
        StringBuilder builder = new StringBuilder();
        builder.Append("Attk: ").Append(characterStats.AttackDamage).Append("\n");
        builder.Append("Crit Dmg: ").Append(characterStats.CriticalDamageStat.Value).Append("\n");
        builder.Append("Crit % : ").Append(characterStats.CriticalChanceStat.Value).Append("\n");
        builder.Append("Attk Speed: ").Append(characterStats.ManualAttackRateStat.Value).Append("\n");
        builder.Append("Magic Dmg: ").Append(characterStats.MagicDamageStat.Value).Append("\n");
        builder.Append("Mag Resist: ").Append(characterStats.MagicalResistanceStat.Value).Append("\n");
        builder.Append("Endur Regen: ").Append(characterStats.EnduranceRegenStat).Append("\n");
        builder.Append("Evasion % : ").Append(characterStats.EvasionChanceStat.Value).Append("\n");
        builder.Append("Item Drop % : ").Append(characterStats.ItemDropRateStat.Value).Append("\n");
        statsText.text = builder.ToString();
    }
}
