using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyGroup : MonoBehaviour
{
    public EnemyController[] enemies;

    private void Awake()
    {
        enemies = GetComponentsInChildren<EnemyController>();
    }

    public void EnableChaseAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.StartChase();
        }
    }
    
}
