using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatModifier
{
    public float Value;
    public object Source;

    public StatModifier(float value, object Source)
    {
        this.Value = value;
        this.Source = Source;
    }

    
}
