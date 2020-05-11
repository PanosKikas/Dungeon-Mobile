using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewShootingInput : MonoBehaviour
{
    
    ProjectileSpawner spawner;

    [SerializeField]
    float rateOfFire = 2f;
    float nextFireTime = 0f;

    [SerializeField]
    LayerMask shootMask;

    private void Awake()
    {
        spawner = GetComponent<ProjectileSpawner>();
    }

    void Update()
    {
        if (PressedShoot() && IsTimeToShoot())
        {
            
            CalculateNextShootTime();
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);

            if (shootMask == (shootMask | (1 << hit.collider.gameObject.layer)))
            {
                 spawner.Spawn();
            }


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
