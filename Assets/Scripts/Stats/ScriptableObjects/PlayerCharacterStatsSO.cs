/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerCharacterStatsSO : CharacterStatsSO
{
    
    private Attribute Vitality = new();
    private Attribute Strength = new();
    private Attribute Agility = new();
    private Attribute Wisdom = new();
    private Attribute Luck = new();

    [HideInInspector]
    public Attribute[] AttributeStats;
    
    public override void Initialize()
    {
        experience = new Experience();
     

        upgradableStatsList.Add(BaseManualAttackRateStat);
        upgradableStatsList.Add(BaseMaxEnduranceStat);
        upgradableStatsList.Add(BaseMaxManaStat);
        upgradableStatsList.Add(BaseEnduranceRegenStat);
        upgradableStatsList.Add(BaseCriticalDamageStat);
        upgradableStatsList.Add(BaseCriticalChanceStat);
        upgradableStatsList.Add(BaseMagicDamageStat);
        upgradableStatsList.Add(BaseEvasionChanceStat);
        upgradableStatsList.Add(BasePhysicalDefenseStat);
        upgradableStatsList.Add(BaseMagicalResistanceStat);
        upgradableStatsList.Add(baseItemDropRateStat);

        Vitality = new Attribute(3);
            
        Strength = new Attribute(3);
        Agility = new Attribute(4);
        Wisdom = new Attribute(1);
        Luck = new Attribute(0);

        AttributeStats = new[]
        {
            Vitality, Strength, Agility, Wisdom, Luck
        };

        MaxHealthStat.AddDependantAttribute(Vitality, new StatModifier(10));
        BaseMaxEnduranceStat.AddDependantAttribute(Vitality, new StatModifier(3));
        BaseEnduranceRegenStat.AddDependantAttribute(Vitality, new StatModifier(.2f));


        AttackDamageStat.AddDependantAttribute(Strength, new StatModifier(1));
        BaseCriticalDamageStat.AddDependantAttribute(Strength, new StatModifier(1));
        AutoAttackRateStat.AddDependantAttribute(Agility, new StatModifier(.1f));
        BaseManualAttackRateStat.AddDependantAttribute(Agility, new StatModifier(.05f));


        BaseEvasionChanceStat.AddDependantAttribute(Luck, new StatModifier(.05f));
        BaseCriticalChanceStat.AddDependantAttribute(Luck, new StatModifier(.05f));
        baseItemDropRateStat.AddDependantAttribute(Luck, new StatModifier(.05f));

    }

    
}
*/
