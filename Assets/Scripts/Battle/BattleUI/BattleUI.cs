using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BattleUI : MonoBehaviour
{   
    protected Slider bar;
    protected StatusEffects statusEffects;
    

    protected virtual void Awake()
    {
        bar = GetComponent<Slider>();

        statusEffects = transform.parent.GetComponentInParent<StatusEffects>();
        
        
    }
    

}
