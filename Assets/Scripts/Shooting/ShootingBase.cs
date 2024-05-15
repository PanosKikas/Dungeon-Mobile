using DMT.Characters.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingBase : MonoBehaviour
{
    protected ProjectileSpawner spawner;

    protected CharacterStats characterStats;

    protected float nextFireTime = 0f;

    [SerializeField]
    LayerMask blockMask;

    private void Awake()
    {
        spawner = GetComponent<ProjectileSpawner>();
    }

    protected virtual void Update()
    {
        if (PressedShoot() && IsTimeToShoot())
        {
            CalculateNextShootTime();
            Vector2 dir = GetShootingDirection();
            spawner.Spawn(dir);
        }
    }

    protected abstract bool PressedShoot();

    protected abstract void CalculateNextShootTime();

    protected abstract bool IsTimeToShoot();

    protected abstract Vector2 GetShootingDirection();

}
