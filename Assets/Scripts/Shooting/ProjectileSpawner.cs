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

    private void Awake()
    {
        projectileDamage = GetComponent<StatusEffects>().stats.ProjecitleDamage;
    }
    public void Spawn()
    {
        CalculateSpawnPositionAndRotation();
        GameObject projectile = Instantiate(projectileToSpawn, bulletSpawnPosition, bulletSpawnRotation);
        projectile.GetComponent<Projectile>().ProjectileDamage = projectileDamage;
    }

    void CalculateSpawnPositionAndRotation()
    {
        Vector2 mouseDirection = GetMouseDirection();
        CalculateSpawnRotation(mouseDirection);
        mouseDirection.Normalize();
        CalculateSpawnPosition(mouseDirection);

    }

    Vector2 GetMouseDirection()
    {
        var mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mouseDir - transform.position;
        return dir;
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
