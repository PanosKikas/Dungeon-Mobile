using DMT.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    bool TryUseOn(Character character);
}
