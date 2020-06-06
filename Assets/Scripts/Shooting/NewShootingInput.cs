using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;


public class NewShootingInput : MonoBehaviour
{
    
    ProjectileSpawner spawner;

    PlayerCharacterStats characterStats;
    
    float nextFireTime = 0f;

    [SerializeField]
    LayerMask blockMask;

    private void Start()
    {
        spawner = GetComponent<ProjectileSpawner>();
        characterStats = StatsDatabase.Instance.GetMainCharacterStats();
    }

    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        

        if (PressedShoot() && IsTimeToShoot())
        {
            
            CalculateNextShootTime();
            

            spawner.Spawn();
        }

    }
    
    bool PressedShoot()
    {
        return Input.GetButtonDown("Fire1");
    }

    bool IsTimeToShoot()
    {
        return Time.time >= nextFireTime;
    }

    void CalculateNextShootTime()
    {
        /// TODO
        nextFireTime = Time.time + 1 / 2f;
    }
}
