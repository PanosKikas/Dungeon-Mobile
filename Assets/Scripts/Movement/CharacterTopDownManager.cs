using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTopDownManager : MonoBehaviour
{
    TopDownMovement movement;
    ShootingBase shootingBase;

    private void Awake()
    {
        movement = GetComponent<TopDownMovement>();
        shootingBase = GetComponent<ShootingBase>();
    }

    public void Freeze()
    {
        shootingBase.enabled = false;
        movement.enabled = false;
    }

    public void Unfreeze()
    {
        shootingBase.enabled = true;
        movement.enabled = true;
    }
}
