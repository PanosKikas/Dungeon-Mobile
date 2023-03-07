using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item, IStorable, IUsable
{
    public bool TryUse()
    {
        _character.Heal(20);
        return true;
    }
}
