using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    public event Action<Condition> OnComplete;


    protected void Complete(){

        OnComplete?.Invoke(this);

    }
}
