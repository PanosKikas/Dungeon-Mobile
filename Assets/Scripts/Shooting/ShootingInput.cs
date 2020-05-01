using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileSpawner))]
public class ShootingInput : MonoBehaviour
{

    ProjectileSpawner spawner;

    float rateOfFire = 2f;
    float nextFireTime = 0f;

    private void Awake()
    {
        spawner = GetComponent<ProjectileSpawner>();
    }

    void Update()
    {
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
        nextFireTime = Time.time + 1 / rateOfFire;
    }   
}
