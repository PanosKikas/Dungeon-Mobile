using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PickupSO : ScriptableObject
{
    
    public string Name;
    public Sprite Icon;
    public GameObject prefab;

    protected PlayerCharacterStats stats;

    public virtual bool Use() 
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterStats>();
        return true; 
    }

    public override string ToString()
    {
        return string.Format("{0}: {1}", Name, Icon);
    }
}
