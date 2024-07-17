using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using DMT.Controllers;
using DMT.Persistent;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGroup : MonoBehaviour
{
    private EnemyController[] enemies;

    private void Awake()
    {
        enemies = GetComponentsInChildren<EnemyController>();
        foreach (var enemy in enemies)
        {
            enemy.EnemyGroup = this;
        }
    }

    public void TargetDetected()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null)
            {
                continue;
            }
            enemy.ChaseTarget();
        }
    }

    public void EngageInCombat(Player player)
    {
        SceneTransitionManager.Instance.TransitionToBattleScene(player.CharacterParty, enemies.Where(e => e != null).Select(e =>  e.Character));
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject, 1f);
        }
    }
}