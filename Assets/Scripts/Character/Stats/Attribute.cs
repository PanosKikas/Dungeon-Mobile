using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Attribute 
{
    [HideInInspector]
    public UnityEvent OnAttributeChanged = new UnityEvent();

    
    int _value;
    
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            OnAttributeChanged?.Invoke();
        }
    }

    public Attribute(int startValue=0)
    {
        _value = startValue;
    }
}
