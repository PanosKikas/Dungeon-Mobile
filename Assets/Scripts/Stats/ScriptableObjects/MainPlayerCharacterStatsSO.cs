using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/MainHeroStats")]
public class MainPlayerCharacterStatsSO : PlayerCharacterStatsSO
{
    public int ProjecitleDamage = 10;
    public float FireRate = 1.5f;
    public int Speed;
    public int InventorySpace = 20;
}
