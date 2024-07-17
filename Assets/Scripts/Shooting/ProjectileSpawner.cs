using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject projectileToSpawn;

    private Vector3 bulletSpawnPosition;
    private Quaternion bulletSpawnRotation;
    
    [SerializeField]
    private LayerMask blockMask;

    public void Spawn(Vector2 dir)
    {
        Vector2 mouseDirection = dir;
        CalculateSpawnRotation(mouseDirection);
        mouseDirection.Normalize();
        CalculateSpawnPosition(mouseDirection);
        Instantiate(projectileToSpawn, bulletSpawnPosition, bulletSpawnRotation);
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
