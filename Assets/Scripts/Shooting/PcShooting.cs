using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;


public class PcShooting : ShootingBase
{    
    private void Start()
    {
        spawner = GetComponent<ProjectileSpawner>();
        //characterStats = StatsDatabase.Instance.GetMainCharacterStats();
    }

    protected override void Update()
    {
        if (EventSystem.current.IsPointerOverUI())
            return;

        base.Update();
    }

    protected override bool PressedShoot()
    {
        return Input.GetButtonDown("Fire1");
    }

    protected override bool IsTimeToShoot()
    {
        return Time.time >= nextFireTime;
    }

    protected override void CalculateNextShootTime()
    {
        nextFireTime = Time.time + 1 / 2f;
    }

    protected override Vector2 GetShootingDirection()
    {
        var mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mouseDir - transform.position;
        return dir;
    }
}
