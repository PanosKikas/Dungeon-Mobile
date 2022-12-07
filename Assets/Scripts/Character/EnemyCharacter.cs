using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    protected override CharacterStats CreateStats()
    {
        return new CharacterStats(Data);
    }
}
