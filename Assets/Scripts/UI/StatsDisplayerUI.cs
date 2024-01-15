using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;

public class StatsDisplayerUI : MonoBehaviour
{
    TextMeshProUGUI statsText;
    PlayerCharacterStatsSO characterStats;

    private void Awake()
    {
        statsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        characterStats = StatsDatabase.Instance.GetMainCharacterStats();
    }

    private void Update()
    {
       
        StringBuilder builder = new StringBuilder();
        builder.Append("Attk: ").Append(characterStats.AttackDamage).Append("\n");
        builder.Append("Crit Dmg: ").Append(characterStats.BaseCriticalDamageStat.Value).Append("\n");
        builder.Append("Crit % : ").Append(characterStats.BaseCriticalChanceStat.Value).Append("\n");
        builder.Append("Attk Speed: ").Append(characterStats.BaseManualAttackRateStat.Value).Append("\n");
        builder.Append("Magic Dmg: ").Append(characterStats.BaseMagicDamageStat.Value).Append("\n");
        builder.Append("Mag Resist: ").Append(characterStats.BaseMagicalResistanceStat.Value).Append("\n");
        builder.Append("Endur Regen: ").Append(characterStats.EnduranceRechargeRate).Append("\n");
        builder.Append("Evasion % : ").Append(characterStats.BaseEvasionChanceStat.Value).Append("\n");
        builder.Append("Item Drop % : ").Append(characterStats.baseItemDropRateStat.Value).Append("\n");
        statsText.text = builder.ToString();
    }
}
