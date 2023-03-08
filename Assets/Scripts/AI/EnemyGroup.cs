using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyGroup : MonoBehaviour
{
    public EnemyBehavior[] enemies;

    private void Awake()
    {
        enemies = GetComponentsInChildren<EnemyBehavior>();
    }

    public void EnableChaseAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.StartChase();
        }
    }
    
}
