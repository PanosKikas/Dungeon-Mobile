using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidShooting : ShootingBase
{
    float rateOfFire = 2f;

    [SerializeField]
    RectTransform blockingRect;

    Touch? currentTouch = null;

    protected override bool PressedShoot()
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

    protected override bool IsTimeToShoot()
    {
        return Time.time >= nextFireTime;
    }

    protected override void CalculateNextShootTime()
    {
        nextFireTime = Time.time + 1 / rateOfFire;
    }

    protected override Vector2 GetShootingDirection()
    {
        var mouseDir = Camera.main.ScreenToWorldPoint(currentTouch.Value.position);

        Vector2 dir = mouseDir - transform.position;
        return dir;
    }
}
