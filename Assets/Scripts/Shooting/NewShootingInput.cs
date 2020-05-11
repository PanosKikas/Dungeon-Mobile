using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class NewShootingInput : MonoBehaviour
{
    
    ProjectileSpawner spawner;

    [SerializeField]
    float rateOfFire = 2f;
    float nextFireTime = 0f;

    [SerializeField]
    LayerMask blockMask;

    private void Awake()
    {
        spawner = GetComponent<ProjectileSpawner>();
    }

    void Update()
    {
        if (PressedShoot() && IsTimeToShoot())
        {
            

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hitArray = Physics2D.RaycastAll(mousePos, Vector2.zero, Mathf.Infinity);

            var query = from hit in hitArray
                        where !hit.collider.isTrigger
                        select hit;


            foreach (var hit in query)
            {
                if (blockMask == (blockMask | (1 << hit.collider.gameObject.layer)))
                {
                    return;
                }
            }
            

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
