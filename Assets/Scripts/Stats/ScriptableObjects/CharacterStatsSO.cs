using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class CharacterStatsSO : ScriptableObject
{
        
    protected List<CharacterStat> upgradableStatsList;

    public CharacterStat MaxHealthStat;
    public CharacterStat AttackDamageStat;

    public CharacterStat AutoAttackRateStat;

    public virtual void Initialize()
    {

        upgradableStatsList = new List<CharacterStat>
        {
           MaxHealthStat, AttackDamageStat, AutoAttackRateStat

        }; 
    }

    #region StatGetters
    public int MaxHealth
    {
        get
        {
            return (int)MaxHealthStat.Value;
        }
        
    }
    
    
    public virtual float AttackDamage
    {
        get
        {
            return AttackDamageStat.Value;
        }
       
    }

    public float AutoAttackRate
    {
        get
        {
            return AutoAttackRateStat.Value;
        }
        
    }
    #endregion


    public GameObject impactEffect;

    [SerializeField]
    Vector3 ImpactEffectOffset;


    public List<CharacterStat> FindCharacterStats(Stat type)
    {
        return upgradableStatsList.FindAll(i => (i.Type == type));
    }

   
}
