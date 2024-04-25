using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ICollectable, IStorable, IEquatable<Item>
{
    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract Sprite Icon { get; }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }
        
        Item other = obj as Item;
        if (other == null)
        {
            return false;
        }
        return this.Equals(other);
    }

    public bool Equals(Item other)
    {
        if (other == null)
        {
            return false;
        }

        return Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
