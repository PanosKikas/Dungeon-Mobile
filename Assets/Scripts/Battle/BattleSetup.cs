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
        // TODO: Delete once we have battle transition working.
        var testStats = new List<CharacterStatsSO>
        {
            BattleTransistor.Instance.enemyGroupStats[0]
        };
        SetupBattleScene(testStats);
        //SetupBattleScene(BattleTransistor.Instance.enemyGroupStats);
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
            enemyToSetup.GetComponent<CharacterBattle>().stats = new CharacterStats(enemyStats[i]);
            enemyToSetup.GetComponentInChildren<Animator>().
                runtimeAnimatorController = enemyStats[i].battleAnimator;
        }
    }

    
    void SetupPlayers()
    {
        for (int i = 0; i < StatsDatabase.Instance.PlayerCharacterStats.Count; ++i)
        {
            var initialData = StatsDatabase.Instance.GetCharacterStats(i);
            playerPlaceholders[i].GetComponent<CharacterBattle>().stats = new PlayerCharacterStats(initialData);
            playerPlaceholders[i].GetComponentInChildren<Animator>().runtimeAnimatorController = initialData.battleAnimator;

        }
    }
}
