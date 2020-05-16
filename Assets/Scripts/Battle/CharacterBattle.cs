using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoAttack))]
public abstract class CharacterBattle: MonoBehaviour 
{
    protected Animator animator;
    public CharacterStats stats;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<AutoAttack>().stats = this.stats;
    }
}
