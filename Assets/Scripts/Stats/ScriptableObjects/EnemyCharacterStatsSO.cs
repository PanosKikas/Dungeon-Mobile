using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/EnemyStats")]
public class EnemyCharacterStatsSO : CharacterStatsSO
{
    public CharacterStat MobilityStat;

    public float Mobility
    {
        get
        {
            return MobilityStat.BaseValue;
        }
        set
        {
            MobilityStat.BaseValue = value;
        }
    }
}
