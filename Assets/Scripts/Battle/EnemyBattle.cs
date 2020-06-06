using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : CharacterBattle
{
    void Start()
    {
        
        stats = GetComponent<CharacterStats>();
    }

}
