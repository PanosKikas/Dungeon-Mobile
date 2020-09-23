using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BattleUI: MonoBehaviour
{   
    protected Slider bar;
    protected CharacterStats stats;
   
    protected virtual void Start()
    {
        bar = GetComponent<Slider>();
        stats = GetComponentInParent<CharacterStats>();
    }
    

}
