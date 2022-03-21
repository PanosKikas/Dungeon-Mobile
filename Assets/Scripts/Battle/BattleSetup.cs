using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSetup : MonoBehaviour
{
    
    [SerializeField]
    GameObject[] enemyPlaceholders;

    [SerializeField]
    GameObject[] playerPlaceholders;

    private void Awake()
    {
        SetupBattleScene(BattleTransistor.Instance.enemyGroupStats);
    }

    public void SetupBattleScene(List<CharacterStatsSO> enemyStats)
    {
        SetupEnemies(enemyStats);
        SetupPlayers();
    }

    void SetupEnemies(List<CharacterStatsSO> enemyStats)
    {
        for (int i = 0; i < enemyStats.Count; ++i)
        {
            GameObject enemyToSetup = enemyPlaceholders[i];
            enemyToSetup.GetComponent<CharacterStats>().Data = enemyStats[i];
            enemyToSetup.GetComponentInChildren<Animator>().
                runtimeAnimatorController = enemyStats[i].battleAnimator;
        }
    }

    
    void SetupPlayers()
    {
        for (int i = 0; i < StatsDatabase.Instance.PlayerCharacterStats.Count; ++i)
        {
            var stat = StatsDatabase.Instance.GetCharacterStats(i);
            playerPlaceholders[i].GetComponent<CharacterStats>().Data = stat;
            playerPlaceholders[i].GetComponentInChildren<Animator>().runtimeAnimatorController = stat.battleAnimator;

        }
    }
}
