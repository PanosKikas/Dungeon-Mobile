using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingInputAndroid : MonoBehaviour
{
    ProjectileSpawner spawner;

    float rateOfFire = 2f;
    float nextFireTime = 0f;

    [SerializeField]
    RectTransform blockingRect;


    Touch? currentTouch = null;

    private void Awake()
    {
        spawner = GetComponent<ProjectileSpawner>();
    }

    void Update()
    {
        if (PressedShoot() && IsTimeToShoot())
        {
            CalculateNextShootTime();
            spawner.SpawnAndroid(currentTouch);
        }

    }

    bool PressedShoot()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && !isTouchingJoystick(touch))
            {
                currentTouch = touch;
                
                return true;
            }
              
        }
        currentTouch = null;
        return false;
    }

    bool isTouchingJoystick(Touch touch)
    {
       return RectTransformUtility.RectangleContainsScreenPoint(blockingRect, touch.position);
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
