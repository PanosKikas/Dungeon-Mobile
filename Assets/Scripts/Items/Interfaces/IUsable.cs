using DMT.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    bool CanBeUsedOn(Character character);
    void UseOn(Character character);
}
