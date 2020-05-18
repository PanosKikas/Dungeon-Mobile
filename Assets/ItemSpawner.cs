using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public void SpawnItem<T>(T item) where T: PickupSO
    {
        GameObject prefab = Instantiate(item.prefab, transform.position + new Vector3(0, -.3f, 0), Quaternion.identity);
        prefab.GetComponent<Pickup<T>>().PickupStats = item;
        prefab.GetComponent<SpriteRenderer>().sprite = item.Icon;
    }
}
