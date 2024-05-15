using DMT.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorable
{
    public string Name { get; }
    public string Description { get; }
    public Sprite Icon { get; }
}
