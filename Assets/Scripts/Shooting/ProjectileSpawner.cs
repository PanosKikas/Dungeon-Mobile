using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject projectileToSpawn;

    Vector3 bulletSpawnPosition;
    Quaternion bulletSpawnRotation;

    int projectileDamage;

    [SerializeField]
    LayerMask blockMask;

    private void Start()
    {
        //projectileDamage = StatsDatabase.Instance.GetMainCharacterStats().ProjecitleDamage;
        projectileDamage = 20;
    }

    public void Spawn(Vector2 dir)
    {
        Vector2 mouseDirection = dir;
        CalculateSpawnRotation(mouseDirection);
        mouseDirection.Normalize();
        CalculateSpawnPosition(mouseDirection);
        GameObject projectile = Instantiate(projectileToSpawn, bulletSpawnPosition, bulletSpawnRotation);
        projectile.GetComponent<Projectile>().ProjectileDamage = projectileDamage;
    }

    void CalculateSpawnRotation(Vector2 mouseDirection)
    {
        bulletSpawnRotation = Quaternion.LookRotation(transform.forward, mouseDirection);
    }

    void CalculateSpawnPosition(Vector2 mouseDirection)
    {
        bulletSpawnPosition = transform.position + new Vector3(mouseDirection.x, mouseDirection.y, 0f);
    }
    
}
