using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : StatusEffects
{
    public float CurrentEndurance { get; private set; }

    protected override void Initialize()
    {
        base.Initialize();
        CurrentEndurance = stats.MaxEndurace;
    }

    public void DecreaseEndurance()
    {
        CurrentEndurance = Mathf.Clamp(CurrentEndurance - stats.EndurancePerAttack, 0, stats.MaxEndurace);
    }

    private void Update()
    {
        RechargeEndurance(Time.deltaTime * stats.EnduranceRechargeRate);
    }

    void RechargeEndurance(float endurance)
    {
        CurrentEndurance = Mathf.Clamp(CurrentEndurance + endurance, 0f, stats.MaxEndurace);
    }

    public override void TakeDamage(int damage, GameObject impactEffect)
    {
        base.TakeDamage(damage, impactEffect);

    }

}
