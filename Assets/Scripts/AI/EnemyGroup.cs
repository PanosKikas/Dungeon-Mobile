using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyGroup : MonoBehaviour
{
    public EnemyFSM[] enemies;
    

    private void Awake()
    {
        enemies = GetComponentsInChildren<EnemyFSM>();
    }

    public void EnableChaseAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.currentState != enemy.ChaseState)
                enemy.ChangeState(enemy.ChaseState);
            else
            {
                Debug.Log(enemy.currentState);
            }
            
        }
    }
    
}
