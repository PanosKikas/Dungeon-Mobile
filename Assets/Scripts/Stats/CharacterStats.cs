using UnityEngine;

[CreateAssetMenu(fileName ="Stats", menuName ="Character/Stats")]
public class CharacterStats : ScriptableObject
{
    //[HideInInspector]
    public int CurrentHealth;
    public int MaxHealth = 500;

    public int ProjecitleDamage = 10;

    public int BaseAttackDamage = 50;

    public float AutoAttackRate = 1 / 2f;
    public float ManualAttackRate = 2f;

    [HideInInspector]
    public float CurrentEndurance;
    public int MaxEndurace = 100;

    public float EndurancePerAttack = 4;

    public float EnduranceRechargeRate = 1f;

    void OnEnable()
    {
        Initialize();
    }

    void Initialize()
    {
        CurrentHealth = MaxHealth;
        CurrentEndurance = MaxEndurace;
    }

}
